using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.History.Dto;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Events.Combo.Delete;
using AlmacenApi.Domain.Repository.History;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.Combo.Delete;

public class DeleteComboEntityCommandHandler : DeleteGenericEntityCommandHandler<ComboEntity, DeleteComboEntityCommand>
{
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    private readonly IHistoryRepository historyRepository;
    private readonly IUserRepository user;
    private readonly IAdminRepository admin;
    public DeleteComboEntityCommandHandler(IComboRepository repository , ILogger<ComboEntity> logger , IHistoryRepository historyRepository , IAdminRepository adminRepository , IUserRepository userRepository) : base(repository)
    {
        comboRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
        admin = adminRepository;
        user = userRepository;
    }
    public override async Task<Result<Unit>> Handle(DeleteComboEntityCommand request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.FindByIdAsync(request.Id , cancellationToken);
        if(combo == null)
        {
            logger.LogWarning("El combo con id: "+request.Id+" no e encuentra");
            return Result<Unit>.Failure(new ComboNotFoundError());
        }
        var DeleteComboDomainEvent = new ComboDeleteEvent(combo.id ,combo.Name);
        combo.AddDomainEvent(DeleteComboDomainEvent);
        await comboRepository.RemoveAsync(combo , cancellationToken);
        if(request.AdminId != null)
        {
            var adminEntity = await admin.FindByIdAsync(request.AdminId , cancellationToken);
            if(adminEntity == null){
                logger.LogWarning("El admin con id: "+request.AdminId+" no se encuentra");
                return Result<Unit>.Failure(new AdminNotFoundError());
            }
            var message = "Se ha eliminado el combo : "+combo.Name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones , adminEntity.Username , message , null);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        if(request.UserId != null)
        {
            var userEntity = await user.FindByIdAsync(request.UserId , cancellationToken);
            if(userEntity == null)
            {
                logger.LogWarning("El user con id: "+request.UserId+" no se encuentra");
                return Result<Unit>.Failure(new UserNotFoundError());
            }
            var message = "Se ha eliminado el combo : "+combo.Name;
            var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones , userEntity.UserName , message , null);
            await historyRepository.AddAsync(history , cancellationToken);
        }
        return Result<Unit>.Success(Unit.Value);
    }
}