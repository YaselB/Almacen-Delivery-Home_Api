using AlmacenApi.Domain.Events.Combo.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.Combo;

public class DeleteComboEventHandler : INotificationHandler<ComboDeleteEvent>
{
    private readonly ILogger<DeleteComboEventHandler> logger;
    public DeleteComboEventHandler(ILogger<DeleteComboEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ComboDeleteEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("El combo con id: "+notification.ComboId+" y nombre: "+notification.ComboName+" ha sido eliminado en la fecha: "+notification.createdAt+" con idEvent: "+notification.Id);
        return Task.CompletedTask;
    }
}