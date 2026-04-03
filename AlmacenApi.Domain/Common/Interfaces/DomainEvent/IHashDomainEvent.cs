// En Domain/Common/IHasDomainEvents.cs
using AlmacenApi.Domain.Interfaces.IDomainEvent;

namespace AlmacenApi.Domain.Common
{
    public interface IHasDomainEvents
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void AddDomainEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
    }
}