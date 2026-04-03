using AlmacenApi.Domain.Events.Combo.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.Combo;

public class CreateComboEventHandler : INotificationHandler<ComboCreateEvent>
{
    private readonly ILogger<CreateComboEventHandler> logger;
    public CreateComboEventHandler(ILogger<CreateComboEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ComboCreateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un combo con id: "+notification.ComboId+" , con nombre: "+notification.ComboName+" , en la fecha: "+notification.createdAt+" con idEvent: "+notification.Id);
        return Task.CompletedTask;
    }
}