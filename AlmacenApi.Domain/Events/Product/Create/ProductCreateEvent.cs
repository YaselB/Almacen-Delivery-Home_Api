using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.Product.Create;

public class ProductCreateEvent : IDomainEvent , INotification
{
    public string Id {get;}

    public DateTime createdAt {get;}
    public string ProductName {get;}
    public string ProductId {get;}
    public ProductCreateEvent(string ProductName , string ProductId)
    {
        this.Id = Guid.NewGuid().ToString();
        this.createdAt = DateTime.UtcNow;
        this.ProductName = ProductName;
        this.ProductId = ProductId;
    }
}