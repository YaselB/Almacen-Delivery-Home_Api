using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Events.AdminEvents.Delete;
using AlmacenApi.Domain.Repository.Generic;
using AlmacenApi.Domain.Repository.History;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.AdminCommand.Delete;

public class DeleteAdminEntityCommandHandler : DeleteGenericEntityCommandHandler<AdminEntity, DeleteAdminEntityCommand>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    private readonly IProductRepository productRepository;
    private readonly IComboRepository comboRepository;
    private readonly IHistoryRepository historyRepository;
    public DeleteAdminEntityCommandHandler(IAdminRepository repository , ILogger<AdminEntity> appLogger , IProductRepository product , IComboRepository combo , IHistoryRepository historyRepository) : base(repository)
    {
        adminRepository = repository;
        logger = appLogger;
        productRepository = product;
        comboRepository = combo;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(DeleteAdminEntityCommand request , CancellationToken cancellationToken)
    {
        var admin = await adminRepository.FindByIdAsync(request.Id , cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("No se puede eliminar el admin "+request.Id+" porque no se encuentra registrado");
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        var product = await productRepository.GetProductByAdmin(admin.id , cancellationToken);
        foreach(var i in product)
        {
            await productRepository.RemoveUserOrAdminId(null , i.CreateByAdmin ,i.id ,cancellationToken);
            logger.LogWarning("El product con id: "+i.id+" se le ha removido el adminId");
        }
        var combo = await comboRepository.GetComboByAdmin(admin.id ,cancellationToken);
        foreach(var i in combo)
        {
            await comboRepository.RemoveUserAndAdmin(null ,i.AdminId ,i.id ,cancellationToken);
            logger.LogWarning("El combo con id: "+i.id+" se le ha removido el adminId");
        }
        var adminDeleteEvent = new DeleteAdminDomainEvent(admin.id , admin.Username);
        admin.AddDomainEvent(adminDeleteEvent);
        await adminRepository.RemoveAsync(admin , cancellationToken);
        var message = "Se ha eliminado el admin: "+admin.Username;
        var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones , admin.Username , message);
        await historyRepository.AddAsync(history , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}