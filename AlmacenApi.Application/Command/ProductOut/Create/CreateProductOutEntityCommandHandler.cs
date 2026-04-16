using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.ProductOut;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
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
    private readonly IUserRepository user;
    private readonly IAdminRepository admin;
    public CreateProductOutEntityCommandHandler(IProductOutRepository repository, IMapper mapper, ILogger<ProductOutEntity> logger, IProductRepository product, IHistoryRepository historyRepository, IUserRepository userRepository, IAdminRepository adminRepository) : base(repository, mapper)
    {
        productOutRepository = repository;
        this.logger = logger;
        productRepository = product;
        this.historyRepository = historyRepository;
        user = userRepository;
        admin = adminRepository;
    }
    public override async Task<Result<Unit>> Handle(CreateProductOutEntityCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning(request.ProductOutDate.ToString());
        var productsIds = request.Products.Select(e => e.Id).ToList();
        var products = await productRepository.GetProductsByIds(productsIds, cancellationToken);
        string UserNameOrAdminName = "";
        if (request.AdminId != null)
        {
            var adminName = await admin.FindByIdAsync(request.AdminId, cancellationToken);
            if (adminName == null)
            {
                logger.LogWarning("El admin que se envio ,no existe");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            UserNameOrAdminName = adminName.Username;
        }
        if (request.UserId != null)
        {
            var userName = await user.FindByIdAsync(request.UserId, cancellationToken);
            if (userName == null)
            {
                logger.LogWarning("El usuario que se envio no existe");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            UserNameOrAdminName = userName.UserName;
        }
        if (products.Count != productsIds.Count)
        {
            logger.LogWarning("Hay productos que no se encuentran ");
            return Result<Unit>.Failure(new ProductsByIdsNotFoundError());
        }
        foreach (var i in products)
        {
            double QuantityToQuit = request.Products.Where(o => o.Id == i.id).Select(e => e.Quantity).FirstOrDefault();
            if (i.Quantity < QuantityToQuit)
            {
                logger.LogWarning("El producto: " + i.name + " no se encuentra");
                return Result<Unit>.Failure(new InsuficientStockOfProductT(i.name));
            }
        }
        string ProductsAndQuantitiesMessage = "Salida de Productos\n";
        var newProducts = new List<ProductOutEntity>();
        foreach (var i in products)
        {
            double QuantityToQuit = request.Products.Where(o => o.Id == i.id).Select(e => e.Quantity).FirstOrDefault();
            i.Quantity -= QuantityToQuit;
            await productRepository.UpdateAsync(i, cancellationToken);
            ProductsAndQuantitiesMessage += "Producto: " + i.name + " , cantidad: " + QuantityToQuit +" , cantidad restante: "+i.Quantity+ "\n";
            var ProductOut = ProductOutEntity.Create(request.UserId, request.AdminId, i.name, QuantityToQuit, request.OutMotive, i.id, request.Customer);
            newProducts.Add(ProductOut);
        }
        ProductsAndQuantitiesMessage += "Cliente: " + request.Customer + "\n" + "Motivo de Salida: " + request.OutMotive;
        await productOutRepository.AddRange(newProducts, cancellationToken);
        var productOutDateUtc = request.ProductOutDate.Kind == DateTimeKind.Utc
    ? request.ProductOutDate
    : DateTime.SpecifyKind(request.ProductOutDate, DateTimeKind.Utc);
    logger.LogWarning(productOutDateUtc.ToString());
        var history = HistoryEntity.Create(HistoryEntity.Type.Salida, UserNameOrAdminName, ProductsAndQuantitiesMessage, productOutDateUtc);
        logger.LogWarning(history.CreatedAt.ToString());
        await historyRepository.AddAsync(history, cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}