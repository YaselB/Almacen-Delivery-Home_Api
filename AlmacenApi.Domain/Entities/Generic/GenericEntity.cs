using System.ComponentModel.DataAnnotations;
using AlmacenApi.Common.Interfaces.Generic;
using AlmacenApi.Domain.Interfaces.IDomainEvent;

namespace AlmacenApi.Domain.Common;
public class GenericEntity<T> : IGenericEntity<T> , IHasDomainEvents{
    [Key]
    public string id{ get ; set ;} = Guid.NewGuid().ToString();
    public DateTime CreatedAt{get ; set ;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get ;set ;} = DateTime.UtcNow;
     private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}