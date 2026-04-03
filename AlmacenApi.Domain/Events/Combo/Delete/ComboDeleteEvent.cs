using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.Combo.Delete;

public class ComboDeleteEvent : IDomainEvent, INotification
{
    public string Id {get ;}
    public DateTime createdAt {get;}
    public string ComboId {get;}
    public string ComboName{get;}
    public ComboDeleteEvent(string ComboId , string ComboName)
    {
        this.ComboId = ComboId;
        this.ComboName = ComboName;
        this.Id = Guid.NewGuid().ToString();
        this.createdAt = DateTime.UtcNow;
    }
}