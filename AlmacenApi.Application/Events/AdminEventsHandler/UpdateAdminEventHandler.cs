using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Events.AdminEvents.Update;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.AdminEventsHandler;

public class UpdateAdminEventHandler : INotificationHandler<AdminUpdateDomainEvent>
{
    private readonly ILogger<UpdateAdminEventHandler> logger;
    public UpdateAdminEventHandler(ILogger<UpdateAdminEventHandler> appLogger)
    {
        logger = appLogger;
    }
    public Task Handle(AdminUpdateDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("El admin con identificador: "+notification.AdminId+" y correo: "+notification.Username+" ha sido actualizado exitosamente");
        return Task.CompletedTask;
    }
}