using AlmacenApi.Domain.Events.Combo.Update;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.Combo;

public class UpdateComboEventHandler : INotificationHandler<ComboUpdateEvent>
{
    private readonly ILogger<UpdateComboEventHandler> logger;
    public UpdateComboEventHandler(ILogger<UpdateComboEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ComboUpdateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se actualizó el combo con id: "+notification.ComboId+" , nombre: "+notification.ComboName+" ,fecha: "+notification.createdAt+" y idEvent: "+notification.Id );
        return Task.CompletedTask;
    }
}