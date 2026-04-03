using AlmacenApi.Domain.Events.Product.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Events.Product;

public class DeleteProductEventHandler : INotificationHandler<ProductDeleteEvent>
{
    private readonly ILogger<DeleteProductEventHandler> logger;
    public DeleteProductEventHandler(ILogger<DeleteProductEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(ProductDeleteEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Producto con nombre: "+notification.ProductName+" y id: "+notification.ProductId+" en la fecha: "+notification.createdAt+" ha sido eliminado exitosamente, con Id del evento: "+notification.Id);
        return Task.CompletedTask;
    }
}