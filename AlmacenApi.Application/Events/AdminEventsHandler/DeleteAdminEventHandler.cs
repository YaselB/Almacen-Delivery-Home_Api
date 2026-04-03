using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Events.AdminEvents.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.AdminEventsHandler;

public class DeleteAdminEventHandler : INotificationHandler<DeleteAdminDomainEvent>
{
    private readonly ILogger<DeleteAdminEventHandler> logger;
    public DeleteAdminEventHandler(ILogger<DeleteAdminEventHandler> appLogger )
    {
        logger = appLogger;
    }
    public Task Handle(DeleteAdminDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("El admin con username: "+notification.UserName+" y identificador: "+notification.AdminId+" ha sido eliminado exitosamente ");
        return Task.CompletedTask;
    }
}