using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.Product.Update;

public class UpdateProductEntityCommandHandler : UpdateGenericEntityCommandHandler<ProductEntity, UpdateProductEntityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    private readonly IHistoryRepository historyRepository;
    public UpdateProductEntityCommandHandler(IProductRepository repository, IMapper mapper , ILogger<ProductEntity> logger , IHistoryRepository historyRepository) : base(repository, mapper)
    {
        productRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(UpdateProductEntityCommand command , CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(command.Id , cancellationToken);
        if(product == null)
        {
            logger.LogWarning("Producto con id: "+command.Id+" no encontrado");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        if(command.AdminId != null && command.UserId != null){
            logger.LogWarning("Se ha intentado actualizar el product con id: "+command.Id+" con id de un admin y de un user");
            return Result<Unit>.Failure(new AdminAndUserError());
        }
        if(command.Quantity == 0)
        {
            var endDateOrdered = product.EndDate.OrderBy(f => f);
            var Time = DateTime.UtcNow;
            foreach(var i in endDateOrdered)
            {
                if(i < Time)
                {
                    product.EndDate.Remove(i);
                }
                if(i > Time)
                {
                    break;
                }
            }
        }
        if(command.AdminId != null)
        {
            product.Update(command.Quantity ,command.endDate , command.AdminId , null);
        }
        else
        {
            product.Update(command.Quantity , command.endDate , null , command.UserId);
        }
        await productRepository.UpdateAsync(product , cancellationToken);
        var message = "se ha agregado una nueva cantidad del producto: "+product.name;
        var history = HistoryEntity.Create(HistoryEntity.Type.Entrada ,product.name ,message);
        await historyRepository.AddAsync(history ,cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}