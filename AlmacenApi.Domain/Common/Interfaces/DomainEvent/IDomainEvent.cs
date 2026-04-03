namespace AlmacenApi.Domain.Interfaces.IDomainEvent;

public interface IDomainEvent
{
    public string Id{get;}
    public DateTime createdAt { get;}
}