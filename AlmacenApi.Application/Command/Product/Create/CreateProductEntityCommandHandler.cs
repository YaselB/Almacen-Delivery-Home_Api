using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.Product.Create;

public class CreateProductEntityCommandHandler : CreateGenericEntityCommandHandler<ProductEntity, CreateProductEntityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    private readonly IHistoryRepository historyRepository;
    public CreateProductEntityCommandHandler(IProductRepository repository, IMapper mapper , ILogger<ProductEntity> logger , IHistoryRepository historyRepository) : base(repository, mapper)
    {
        productRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(CreateProductEntityCommand command  , CancellationToken cancellationToken)
    {
        var productName = await productRepository.GetProductByName(command.ProductName , cancellationToken);
        if(productName != null)
        {
            logger.LogWarning("El producto con ese nombre ya existe");
            return Result<Unit>.Failure(new ProductRegisteredError());
        }
        if(command.AdminId == null)
        {
            var product = ProductEntity.Create(command.UserId ,null ,command.ProductName ,command.Quantity ,command.Unity ,command.endDate , command.Category);
            await productRepository.AddAsync(product ,cancellationToken);
            var message = "Se ha creado un nuevo producto: "+product.name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones ,product.name ,message);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        if(command.AdminId != null)
        {
            var product = ProductEntity.Create(null , command.AdminId , command.ProductName , command.Quantity ,command.Unity ,command.endDate , command.Category);
            await productRepository.AddAsync(product ,cancellationToken);
            var message = "Se ha creado un nuevo producto: "+product.name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones ,product.name ,message);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}