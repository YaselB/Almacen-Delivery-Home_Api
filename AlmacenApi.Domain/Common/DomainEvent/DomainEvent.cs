using AlmacenApi.Domain.Interfaces.IDomainEvent;

namespace AlmacenApi.Common.DomainEvent;

public class DomainEvent : IDomainEvent
{
    public string Id => Guid.NewGuid().ToString();
    public DateTime createdAt => DateTime.UtcNow;
}
