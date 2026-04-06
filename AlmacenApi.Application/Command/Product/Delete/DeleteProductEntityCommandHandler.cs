using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
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
    private readonly IAdminRepository admin;
    private readonly IUserRepository user;
    public DeleteProductEntityCommandHandler(IProductRepository repository, ILogger<ProductEntity> logger, IHistoryRepository historyRepository, IAdminRepository adminRepository, IUserRepository userRepository) : base(repository)
    {
        productRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
        admin = adminRepository;
        user = userRepository;
    }
    public override async Task<Result<Unit>> Handle(DeleteProductEntityCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(request.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("El producto con id: " + request.Id + " no se encuentra");
            return Result<Unit>.Failure(new ProductNotFoundError());
        }
        var DeleteProductDomainEvent = new ProductDeleteEvent(product.id, product.name);
        product.AddDomainEvent(DeleteProductDomainEvent);
        await productRepository.RemoveAsync(product, cancellationToken);
        if (request.AdminId != null)
        {
            var adminEntity = await admin.FindByIdAsync(request.AdminId, cancellationToken);
            if (adminEntity == null)
            {
                logger.LogWarning("El admin con id: " + request.AdminId + " no se encuentra");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            var message = "Se ha eliminado el producto: " + product.name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones, adminEntity.Username , message);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        if(request.UserId != null)
        {
            var userEntity = await user.FindByIdAsync(request.UserId ,cancellationToken);
            if(userEntity == null)
            {
                logger.LogWarning("El usuario con id: "+request.UserId+" no se encuentra");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            var message = "Se ha eliminado el producto: " + product.name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones, userEntity.UserName , message);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}