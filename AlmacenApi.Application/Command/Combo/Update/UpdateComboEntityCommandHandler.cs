using AlmacenApi.Aplication.Command.Generic.Update;
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

namespace AlmacenApi.Aplication.Command.Combo.Update;

public class UpdateComboEntityCommandHandler : UpdateGenericEntityCommandHandler<ComboEntity, UpdateComboEntityCommand>
{
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    private readonly IHistoryRepository historyRepository;
    private readonly IProductRepository productRepository;
    private readonly IProductComboRepository productComboRepository;
    private readonly IUserRepository user;
    private readonly IAdminRepository admin;
    public UpdateComboEntityCommandHandler(IComboRepository repository, IMapper mapper, ILogger<ComboEntity> logger, IHistoryRepository historyRepository, IProductRepository product, IProductComboRepository productCombo, IAdminRepository adminRepository, IUserRepository userRepository) : base(repository, mapper)
    {
        this.logger = logger;
        comboRepository = repository;
        this.historyRepository = historyRepository;
        this.productRepository = product;
        productComboRepository = productCombo;
        user = userRepository;
        admin = adminRepository;
    }
    public override async Task<Result<Unit>> Handle(UpdateComboEntityCommand request, CancellationToken cancellationToken)
    {
        // 1. Validar que el combo existe
        var combo = await comboRepository.FindByIdAsync(request.Id, cancellationToken);
        if (combo == null)
        {
            logger.LogWarning("El combo con id: " + request.Id + " no se encuentra");
            return Result<Unit>.Failure(new ComboNotFoundError());
        }

        // 2. Validar que no se envíen adminId y userId a la vez
        if (request.AdminId != null && request.UserId != null)
        {
            logger.LogWarning("Se está intentando actualizar un combo con un usuario y un admin");
            return Result<Unit>.Failure(new ComboUserAndAdminIdError());
        }

        // 3. Obtener los productos existentes según los IDs solicitados
        var productIds = request.ProductsIds.Select(p => p.Id).ToList();
        var products = await productRepository.GetProductsByIds(productIds, cancellationToken);
        if (products.Count != productIds.Count)
        {
            logger.LogWarning("Hay productos que no se encuentran registrados");
            return Result<Unit>.Failure(new ProductsByIdsNotFoundError());
        }

        // 4. Obtener las relaciones actuales del combo
        var existingRelations = await productComboRepository.GetProductsOfTheCombo(combo.id, cancellationToken);
        // Convertir a diccionario para fácil acceso (ProductId -> ProductComboEntity)
        var existingDict = existingRelations.ToDictionary(r => r.ProductId);

        // 5. Diccionario con las cantidades solicitadas
        var requestedDict = request.ProductsIds.ToDictionary(p => p.Id, p => p.Quantity);

        // 6. Procesar cada relación existente
        foreach (var relation in existingRelations)
        {
            if (requestedDict.ContainsKey(relation.ProductId))
            {
                // El producto sigue en el combo -> actualizar cantidad
                var newQuantity = requestedDict[relation.ProductId];
                if (relation.Quantity != newQuantity)
                {
                    relation.Quantity = newQuantity;
                    await productComboRepository.UpdateAsync(relation, cancellationToken);
                }
                // Marcar como procesado para no crearlo de nuevo
                requestedDict.Remove(relation.ProductId);
            }
            else
            {
                // El producto ya no está en la solicitud -> eliminar relación
                await productComboRepository.RemoveAsync(relation, cancellationToken);
            }
        }

        // 7. Crear nuevas relaciones para los productos que no existían
        foreach (var newProduct in requestedDict)
        {
            var newRelation = ProductComboEntity.Create(combo.id, newProduct.Key, newProduct.Value);
            await productComboRepository.AddAsync(newRelation, cancellationToken);
        }

        // 8. Actualizar los datos básicos del combo
        combo.Update(request.Name, request.AdminId, request.UserId);
        await comboRepository.UpdateAsync(combo, cancellationToken);
        if (request.AdminId != null)
        {
            var adminEntity = await admin.FindByIdAsync(request.AdminId, cancellationToken);
            if (adminEntity == null)
            {
                logger.LogWarning("El admin con id: " + request.AdminId + " no se encuentra");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            // 9. Registrar en el historial
            var message = "Se ha actualizado el combo: " + combo.Name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Modificaciones, adminEntity.Username, message , null);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        if(request.UserId != null){
            var userEntity = await user.FindByIdAsync(request.UserId , cancellationToken);
            if(userEntity == null)
            {
                logger.LogWarning("El user con id: "+request.UserId+" no se encuentra");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            var message = "Se ha actualizado el combo: " + combo.Name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Modificaciones, userEntity.UserName, message , null);
            await historyRepository.AddAsync(history, cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}