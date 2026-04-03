using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.Combo.Create;

public class ComboCreateEvent : IDomainEvent, INotification
{
    public string Id { get ;}

    public DateTime createdAt {get ;}
    public string ComboId { get;}
    public string ComboName {get ;}
    public ComboCreateEvent(string comboId , string comboName)
    {
        Id = Guid.NewGuid().ToString();
        createdAt = DateTime.UtcNow;
        this.ComboId = comboId;
        this.ComboName = comboName;
    }
}