using AlmacenApi.Domain.Events.ProductOut.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.ProductOut;

public class CreateProductOutEventHandler : INotificationHandler<ProductOutCreate>
{
    private readonly ILogger<CreateProductOutEventHandler> logger;
    public CreateProductOutEventHandler(ILogger<CreateProductOutEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ProductOutCreate notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado unasalida de producto con id:"+notification.ProductOutId+" , nombre de producto: "+notification.ProductName+" ,fecha: "+notification.createdAt+" y idEvent:"+notification.Id);
        return Task.CompletedTask;
    }
}