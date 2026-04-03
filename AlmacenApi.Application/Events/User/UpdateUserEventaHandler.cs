using AlmacenApi.Domain.Events.User.Update;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.User;

public class UpdateUserEventHandler : INotificationHandler<UserUpdateEvent>
{
    private readonly ILogger<UpdateUserEventHandler> logger;
    public UpdateUserEventHandler(ILogger<UpdateUserEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UserUpdateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("El usuario con username:"+notification.UserName+" y id: "+notification.UserId+" ha sido actualizado a la hora :"+notification.createdAt);
        return Task.CompletedTask;
    }
}