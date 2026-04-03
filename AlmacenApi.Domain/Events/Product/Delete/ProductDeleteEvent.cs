using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.Product.Delete;

public class ProductDeleteEvent : IDomainEvent, INotification
{
    public string Id {get;}

    public DateTime createdAt {get;}
    public string ProductId{get;}
    public string ProductName{get;}
    public ProductDeleteEvent(string ProductId , string ProductName)
    {
        this.Id = Guid.NewGuid().ToString();
        this.createdAt = DateTime.UtcNow;
        this.ProductId = ProductId;
        this.ProductName = ProductName;
    }
}