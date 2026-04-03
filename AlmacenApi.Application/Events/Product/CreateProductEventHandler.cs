using AlmacenApi.Domain.Events.Product.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.Product;

public class CreateProductEventHandler : INotificationHandler<ProductCreateEvent>
{
    private readonly ILogger<CreateProductEventHandler> logger;
    public CreateProductEventHandler(ILogger<CreateProductEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ProductCreateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un nuevo Producto con nombre: "+notification.ProductName+" , con Id: "+notification.ProductId+" , fecha: "+notification.createdAt+" y idEvent: "+notification.Id);
        return Task.CompletedTask;
    }
}