using AlmacenApi.Domain.Events.User.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.User;

public class CreateUserEventHandler : INotificationHandler<UserCreateEvent>
{
    private readonly ILogger<CreateUserEventHandler> logger;
    public CreateUserEventHandler(ILogger<CreateUserEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UserCreateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "El usuario se ha creado correctamente con este username: "+notification.UserName+" , id: "+notification.UserId+" y hora: "+notification.createdAt+"."
        );
        return Task.CompletedTask;
    }
}