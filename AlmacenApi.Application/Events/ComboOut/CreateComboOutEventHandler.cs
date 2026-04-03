using AlmacenApi.Domain.Events.ComboOut;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.ComboOut;

public class CreateComboOutEventHandler : INotificationHandler<ComboOutCreateEvent>
{
    private readonly ILogger<CreateComboOutEventHandler> logger;
    public CreateComboOutEventHandler(ILogger<CreateComboOutEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ComboOutCreateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado una salida de combo para el combo: "+notification.ComboName+" , id: "+notification.ComboId+" fecha: "+notification.createdAt+" y idEvent: "+notification.Id);
        return Task.CompletedTask;
    }
}