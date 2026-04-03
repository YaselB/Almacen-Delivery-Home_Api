using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Events.Product.Delete;
using AlmacenApi.Domain.Repository.History;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.Product.Delete;

public class DeleteProductEntityCommandHandler : DeleteGenericEntityCommandHandler<ProductEntity, DeleteProductEntityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    private readonly IHistoryRepository historyRepository;
    public DeleteProductEntityCommandHandler(IProductRepository repository , ILogger<ProductEntity> logger , IHistoryRepository historyRepository) : base(repository)
    {
        productRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(DeleteProductEntityCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(request.Id , cancellationToken);
        if(product == null)
        {
            logger.LogWarning("El producto con id: "+request.Id+" no se encuentra");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        var DeleteProductDomainEvent = new ProductDeleteEvent(product.id , product.name);
        product.AddDomainEvent(DeleteProductDomainEvent);
        await productRepository.RemoveAsync(product , cancellationToken);
        var message = "Se ha eliminado el producto: "+product.name;
        var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones ,product.name , message);
        await historyRepository.AddAsync(history , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}