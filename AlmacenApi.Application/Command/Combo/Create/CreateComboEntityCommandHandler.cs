using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Common.Interfaces.Repository.ProductComboRepository;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.ProductCombo;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.Combo.Create;

public class CreteComboEntityCommandHandler : CreateGenericEntityCommandHandler<ComboEntity, CreateComboEntityCommand>
{
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    private readonly IProductRepository productRepository;
    private readonly IProductComboRepository productComboRepository;
    private readonly IHistoryRepository historyRepository;
    private readonly IAdminRepository adminRepository;
    private readonly IUserRepository userRepository;
    public CreteComboEntityCommandHandler(IComboRepository repository, IMapper mapper , ILogger<ComboEntity> logger , IProductRepository product , IProductComboRepository productComboRepository , IHistoryRepository historyRepository , IAdminRepository admin , IUserRepository userRepository) : base(repository, mapper)
    {
        comboRepository = repository;
        this.logger = logger;
        productRepository = product;
        this.productComboRepository = productComboRepository;
        this.historyRepository = historyRepository;
        this.adminRepository = admin;
        this.userRepository = userRepository;
    }
    public override async Task<Result<Unit>> Handle(CreateComboEntityCommand request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.GetByName(request.Name , cancellationToken);
        if(combo != null)
        {
            logger.LogWarning("El combo con nombre: "+request.Name+" ya existe ");
            return Result<Unit>.Failure(new ComboRegisteredError());
        }
        if(request.AdminId == null && request.UserId == null)
        {
            logger.LogError("No se puede crear un product sin un AdminId o UserId");
            return Result<Unit>.Failure(new ComboUserAndAdminIdError());
        }
        var products = await productRepository.GetProductsByIds(request.products.Select(p => p.Id).ToList(), cancellationToken);
        if(request.products.Count != products.Count)
        {
            logger.LogWarning("Algunos productos no se encontraron a la hora de crear los combos");
            return Result<Unit>.Failure(new ProductsByIdsNotFoundError());
        }
        if(request.AdminId != null)
        {
            var newCombo = ComboEntity.Create(request.Name ,request.AdminId , null);
            var productCombo = new List<ProductComboEntity>();
            foreach(var i in products)
            {
                var Quantity = request.products.Where(p => p.Id == i.id).Select(p => p.Quantity).FirstOrDefault();
                var newProductCombo = new ProductComboEntity
                {
                    ComboId = newCombo.id,
                    ProductId = i.id,
                    Quantity = Quantity
                };
                productCombo.Add(newProductCombo);
            }            
            await comboRepository.AddAsync(newCombo , cancellationToken);
            await productComboRepository.AddRange(productCombo , cancellationToken);
            var adminName = await adminRepository.FindByIdAsync(request.AdminId , cancellationToken);
            if(adminName == null)
            {
                logger.LogWarning("El admin con id : "+request.AdminId+" no se encuentra");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            var message = "Se ha creado un combo: "+newCombo.Name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones , adminName.Username , message , null);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        if(request.UserId != null)
        {
            var newCombo = ComboEntity.Create(request.Name ,null ,request.UserId);
            var productCombo = new List<ProductComboEntity>();
            foreach(var i in products)
            {
                var Quantity = request.products.Where(p => p.Id == i.id).Select(p => p.Quantity).FirstOrDefault();
                var newProductCombo = new ProductComboEntity
                {
                    ComboId = newCombo.id,
                    ProductId = i.id,
                    Quantity = Quantity
                };
                productCombo.Add(newProductCombo);
            }
            await comboRepository.AddAsync(newCombo ,cancellationToken);
            await productComboRepository.AddRange(productCombo , cancellationToken);
           var userName = await userRepository.FindByIdAsync(request.UserId , cancellationToken);
            if(userName == null)
            {
                logger.LogWarning("El usuario con id : "+request.UserId+" no se encuentra");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            var message = "Se ha creado un combo: "+newCombo.Name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones , userName.UserName , message , null);
            await historyRepository.AddAsync(history , cancellationToken); 
        }
        return Result<Unit>.Success(Unit.Value);
    }
}
