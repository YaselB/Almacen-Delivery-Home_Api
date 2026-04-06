using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.ComboOutDtoClass;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Common.Interfaces.Repository.ComboOut;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.CombOut;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.ComboOut.Create;

public class CreateComboOutEntityCommandHandler
    : CreateGenericEntityCommandHandler<ComboOutEntity, CreateComboOutEntityCommand>
{
    private readonly IComboOutRepository comboOutRepository;
    private readonly IComboRepository comboRepository;
    private readonly IProductRepository productRepository;
    private readonly ILogger<ComboOutEntity> logger;
    private readonly IHistoryRepository historyRepository;
    private readonly IAdminRepository admin;
    private readonly IUserRepository user;

    public CreateComboOutEntityCommandHandler(
        IComboOutRepository repository,
        IMapper mapper,
        ILogger<ComboOutEntity> logger,
        IProductRepository product,
        IComboRepository combo,
        IHistoryRepository historyRepository,
        IAdminRepository adminRepository,
        IUserRepository userRepository)
        : base(repository, mapper)
    {
        this.comboOutRepository = repository;
        this.comboRepository = combo;
        this.productRepository = product;
        this.logger = logger;
        this.historyRepository = historyRepository;
        admin = adminRepository;
        user = userRepository;
    }

    public override async Task<Result<Unit>> Handle(CreateComboOutEntityCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar que no se envíen adminId y userId juntos
        if (request.UserId != null && request.AdminId != null)
        {
            return Result<Unit>.Failure(new AdminAndUserError());
        }

        // 2. Validar que exista el combo
        var combo = await comboRepository.FindByIdAsync(request.ComboId, cancellationToken);
        if (combo == null)
        {
            return Result<Unit>.Failure(new ComboNotFoundError());
        }

        // 3. Validar que se hayan enviado productos
        if (request.ComboEntity == null || !request.ComboEntity.Any())
        {
            return Result<Unit>.Failure(new ProductsNotFoundInTheCombo());
        }

        // 4. Obtener los productos según los IDs enviados
        var productIds = request.ComboEntity.Select(c => c.ProductDto).ToList();
        var products = await productRepository.GetProductsByIds(productIds, cancellationToken);
        if (products.Count != productIds.Count)
        {
            return Result<Unit>.Failure(new ProductsByIdsNotFoundError());
        }

        // 5. Validar stock y descontar individualmente
        var erroresStock = new List<string>();
        foreach (var item in request.ComboEntity)
        {
            var product = products.FirstOrDefault(p => p.id == item.ProductDto);
            if (product == null)
            {
                erroresStock.Add($"Producto con ID {item.ProductDto} no encontrado");
                continue;
            }
            if (product.Quantity < item.Quantity)
            {
                erroresStock.Add($"Stock insuficiente para {product.name}: disponible {product.Quantity}, solicitado {item.Quantity}");
                continue;
            }
            // Descontar stock
            product.Quantity -= item.Quantity;
            await productRepository.UpdateAsync(product, cancellationToken);
        }

        if (erroresStock.Any())
        {
            return Result<Unit>.Failure(new InsuficientProductStockError());
        }

        // 6. Crear la entidad de salida de combo con los productos y cantidades reales
        var comboOutItems = request.ComboEntity.Select(c => new ComboOutDto
        {
            ProductId = c.ProductDto,
            Quantity = c.Quantity
        }).ToList();

        var newComboOut = ComboOutEntity.Create(
            request.ComboId,
            request.UserId,
            request.AdminId,
            request.OutMotive,
            combo.Name,
            comboOutItems,
            request.Customer
        );

        await comboOutRepository.AddAsync(newComboOut, cancellationToken);
        if (request.AdminId != null)
        {
            var adminEntity = await admin.FindByIdAsync(request.AdminId, cancellationToken);
            if (adminEntity == null)
            {
                logger.LogWarning("El admin con id: " + request.AdminId + " no se encuentra");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            var message = $"Se ha creado una salida personalizada para el combo: {combo.Name}";
            var history = HistoryEntity.Create(HistoryEntity.Type.Salida, adminEntity.Username, message);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        if(request.UserId != null)
        {
            var userEntity = await user.FindByIdAsync(request.UserId , cancellationToken);
            if(userEntity == null)
            {
                logger.LogWarning("El usuario con id: "+request.UserId+" no e encuentra");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            var message = $"Se ha creado una salida personalizada para el combo: {combo.Name}";
            var history = HistoryEntity.Create(HistoryEntity.Type.Salida, userEntity.UserName, message);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}