using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Events.User.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.User;

public class DeleteUserEventHandler : INotificationHandler<DeleteUserEvent>
{
    private readonly ILogger<DeleteUserEventHandler> logger;
    public DeleteUserEventHandler(ILogger<DeleteUserEventHandler> logger )
    {
        this.logger = logger;
    }
    public Task Handle(DeleteUserEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("El usuario con Username: "+notification.UserName+" y id: "+notification.UserId+" ha sido eliminado exitosamente a la hora: "+notification.createdAt);
        return Task.CompletedTask;
    }
}