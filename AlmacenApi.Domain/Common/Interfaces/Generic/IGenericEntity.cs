using AlmacenApi.Domain.Interfaces.IDomainEvent;

namespace AlmacenApi.Common.Interfaces.Generic;
public interface IGenericEntity<T>
{
    public string id{get; set;}
    public DateTime CreatedAt{ get; set;}
    public DateTime UpdatedAt {get; set;}

}