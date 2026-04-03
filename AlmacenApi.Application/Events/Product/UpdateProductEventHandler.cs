using AlmacenApi.Domain.Events.Product.Update;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.Product;

public class UpdateProductEventHandler : INotificationHandler<ProductUpdateEvent>
{
    private readonly ILogger<UpdateProductEventHandler> logger;
    public UpdateProductEventHandler(ILogger<UpdateProductEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ProductUpdateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el producto: "+notification.ProductName+" con id: "+notification.ProductId
        +" en la fecha: "+notification.createdAt+" y idEvent: "+notification.Id);
        return Task.CompletedTask;
    }
}