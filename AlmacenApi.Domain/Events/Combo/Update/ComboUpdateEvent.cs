using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.Combo.Update;

public class ComboUpdateEvent : IDomainEvent, INotification
{
    public string Id { get ;}
    public DateTime createdAt {get;}
    public string ComboId {get ;}
    public string ComboName { get ;}
    public ComboUpdateEvent(string comboId , string comboName)
    {
        this.ComboId = comboId;
        this.ComboName = comboName;
        this.Id = Guid.NewGuid().ToString();
        this.createdAt = DateTime.UtcNow;
    }
}