using System.Net.Http.Headers;
using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.ProductOut;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.ProductOut.Create;

public class CreateProductOutEntityCommandHandler : CreateGenericEntityCommandHandler<ProductOutEntity, CreateProductOutEntityCommand>
{
    private readonly IProductOutRepository productOutRepository;
    private readonly ILogger<ProductOutEntity> logger;
    private readonly IProductRepository productRepository;
    private readonly IHistoryRepository historyRepository;
    public CreateProductOutEntityCommandHandler(IProductOutRepository repository, IMapper mapper , ILogger<ProductOutEntity> logger , IProductRepository product , IHistoryRepository historyRepository) : base(repository, mapper)
    {
        productOutRepository = repository;
        this.logger = logger;
        productRepository = product;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(CreateProductOutEntityCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(request.ProductId , cancellationToken);
        if(product == null)
        {
            logger.LogWarning("Ese producto no esta registrado");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        if(request.Quantity > product.Quantity)
        {
            logger.LogWarning("No se puede cumplir con el pedido porque no hay suficiente en stock ");
            return Result<Unit>.Failure(new InsuficientProductStockError());
        }
        product.Quantity -= request.Quantity;
        if(request.AdminId != null)
        {
            var productOut = ProductOutEntity.Create(null ,request.AdminId , product.name ,request.Quantity ,request.OutMotive ,product.id ,request.Customer);
            await productOutRepository.AddAsync(productOut, cancellationToken);
            await productRepository.UpdateAsync(product , cancellationToken);
            var message = "Se le ha dado salida al producto: "+product.name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Entrada ,product.name , message);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        if(request.UserId != null)
        {
            var productOut = ProductOutEntity.Create(request.UserId ,null , product.name ,request.Quantity ,request.OutMotive ,product.id , request.Customer);
            await productOutRepository.AddAsync(productOut, cancellationToken);
            await productRepository.UpdateAsync(product , cancellationToken);
            var message = "Se le ha dado salida al producto: "+product.name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Salida ,product.name , message);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}