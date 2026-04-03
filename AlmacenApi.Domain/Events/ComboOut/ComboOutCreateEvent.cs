using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.ComboOut;

public class ComboOutCreateEvent : IDomainEvent, INotification
{
    public string Id {get;}

    public DateTime createdAt {get;}
    public string ComboName{get;}
    public string ComboId {get;}
    public ComboOutCreateEvent(string ComboName , string ComboId)
    {
      this.Id = Guid.NewGuid().ToString();
      this.createdAt = DateTime.UtcNow;
      this.ComboName = ComboName;
      this.ComboId = ComboId;  
    }
}