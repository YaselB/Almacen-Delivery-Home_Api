using AlmacenApi.Aplication.Command.Generic.Create;
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

namespace AlmacenApi.Aplication.Command.Product.Create;

public class CreateProductEntityCommandHandler : CreateGenericEntityCommandHandler<ProductEntity, CreateProductEntityCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    private readonly IHistoryRepository historyRepository;
    private readonly IAdminRepository admin;
    private readonly IUserRepository user;
    public CreateProductEntityCommandHandler(IProductRepository repository, IMapper mapper , ILogger<ProductEntity> logger , IHistoryRepository historyRepository , IAdminRepository adminRepository , IUserRepository userRepository) : base(repository, mapper)
    {
        productRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
        admin = adminRepository;
        user = userRepository;
    }
    public override async Task<Result<Unit>> Handle(CreateProductEntityCommand command  , CancellationToken cancellationToken)
    {
        var productName = await productRepository.GetProductByName(command.ProductName , cancellationToken);
        if(productName != null)
        {
            logger.LogWarning("El producto con ese nombre ya existe");
            return Result<Unit>.Failure(new ProductRegisteredError());
        }
        if(command.UserId != null)
        {
            var product = ProductEntity.Create(command.UserId ,null ,command.ProductName ,command.Quantity ,command.Unity ,command.endDate , command.Category);
            await productRepository.AddAsync(product ,cancellationToken);
            var userEntity = await user.FindByIdAsync(command.UserId , cancellationToken);
            if(userEntity == null)
            {
                logger.LogWarning("El usuario con id: "+command.UserId+" no se encuentra");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            var message = "Se ha creado un nuevo producto: "+product.name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones ,userEntity.UserName ,message);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        if(command.AdminId != null)
        {
            var product = ProductEntity.Create(null , command.AdminId , command.ProductName , command.Quantity ,command.Unity ,command.endDate , command.Category);
            await productRepository.AddAsync(product ,cancellationToken);
            var adminEntity = await admin.FindByIdAsync(command.AdminId , cancellationToken);
            if(adminEntity == null)
            {
                logger.LogWarning("El admin con id: "+command.AdminId+" no se encuentra");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            var message = "Se ha creado un nuevo producto: "+product.name+" con un cantidad de: "+product.Quantity+product.Unity;
            var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones ,adminEntity.Username ,message);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}