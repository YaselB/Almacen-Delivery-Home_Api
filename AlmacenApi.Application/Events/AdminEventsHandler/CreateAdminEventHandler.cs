using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Events.AdminEvents.Create;
using AlmacenApi.Domain.Repository.History;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.AdminEventsHandler;

public class CreateAdminEVentHandler : INotificationHandler<AdminCreateDomainEvents>
{
    private readonly ILogger<CreateAdminEVentHandler> logger;

    public CreateAdminEVentHandler(ILogger<CreateAdminEVentHandler> logger)
    {
        this.logger = logger;
        
    }
    public Task Handle(AdminCreateDomainEvents notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
                "Admin creado exitosamente - ID: {AdminId}, UserName: {Username}, Fecha: {CreatedAt}",
                notification.AdminId,
                notification.Username,
                notification.createdAt
            );
        return Task.CompletedTask;
    }
}