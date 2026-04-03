using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.ProductOut.Create;

public class ProductOutCreate : IDomainEvent, INotification
{
    public string Id {get;}

    public DateTime createdAt {get;}
    public string ProductName{get;}
    public string ProductOutId{get;}
    public ProductOutCreate(string ProductName , string ProductOutId)
    {
        this.Id = Guid.NewGuid().ToString();
        this.createdAt = DateTime.UtcNow;
        this.ProductName = ProductName;
        this.ProductOutId = ProductOutId;
    }
}