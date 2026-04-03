using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.User.Create;

public class UserCreateEvent : IDomainEvent, INotification
{
    public string Id {get;}

    public DateTime createdAt {get;}
    public string UserName{get;}
    public string UserId{get;}
    public UserCreateEvent(string userName , string id)
    {
        this.UserId = id;
        this.UserName = userName;
        createdAt = DateTime.UtcNow;
        Id = Guid.NewGuid().ToString();
    }
}