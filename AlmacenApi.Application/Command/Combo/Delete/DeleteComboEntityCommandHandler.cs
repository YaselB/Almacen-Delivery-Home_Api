using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.History.Dto;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
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
    public DeleteComboEntityCommandHandler(IComboRepository repository , ILogger<ComboEntity> logger , IHistoryRepository historyRepository) : base(repository)
    {
        comboRepository = repository;
        this.logger = logger;
        this.historyRepository = historyRepository;
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
        var message = "Se ha eliminado un combo : "+combo.Name;
        var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones , combo.Name , message);
        await historyRepository.AddAsync(history , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}