using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
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
    private readonly IUserRepository user;
    private readonly IAdminRepository admin;
    public UpdateProductEntityCommandHandler(IProductRepository repository, IMapper mapper, ILogger<ProductEntity> logger, IHistoryRepository historyRepository, IUserRepository userRepository, IAdminRepository adminRepository) : base(repository, mapper)
    {
        productRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
        user = userRepository;
        admin = adminRepository;
    }
    public override async Task<Result<Unit>> Handle(UpdateProductEntityCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(command.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Producto con id: " + command.Id + " no encontrado");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        if (command.AdminId != null && command.UserId != null)
        {
            logger.LogWarning("Se ha intentado actualizar el product con id: " + command.Id + " con id de un admin y de un user");
            return Result<Unit>.Failure(new AdminAndUserError());
        }
        if (command.Quantity == 0)
        {
            var endDateOrdered = product.EndDate.OrderBy(f => f);
            var Time = DateTime.UtcNow;
            foreach (var i in endDateOrdered)
            {
                if (i < Time)
                {
                    product.EndDate.Remove(i);
                }
                if (i > Time)
                {
                    break;
                }
            }
        }
        if (command.AdminId != null)
        {
            product.Update(command.Quantity, command.endDate, command.AdminId, null , command.Provider);
            var adminEntity = await user.FindByIdAsync(command.AdminId, cancellationToken);
            if (adminEntity == null)
            {
                logger.LogWarning("El admin con id: " + command.AdminId + " no se encuentra");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            await productRepository.UpdateAsync(product, cancellationToken);
            var message = "se ha agregado una nueva cantidad del producto: " + product.name+" ,"+command.Quantity+product.Unity+" por el proveedor:"+command.Provider;
            var history = HistoryEntity.Create(HistoryEntity.Type.Entrada, adminEntity.UserName, message , null);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        if(command.UserId != null)
        {
            product.Update(command.Quantity, command.endDate, null, command.UserId , command.Provider);
            var userEntity = await user.FindByIdAsync(command.UserId ,cancellationToken);
            if(userEntity == null)
            {
                logger.LogWarning("El usuario con id: "+command.UserId+" no se encuentra");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            await productRepository.UpdateAsync(product, cancellationToken);
            var message = "se ha agregado una nueva cantidad del producto: " + product.name+" ,"+command.Quantity+product.Unity+" por el proveedor:"+command.Provider;
            var history = HistoryEntity.Create(HistoryEntity.Type.Entrada, userEntity.UserName, message , null);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}