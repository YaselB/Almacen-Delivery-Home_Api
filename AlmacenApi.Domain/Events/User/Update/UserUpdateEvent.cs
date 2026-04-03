using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.User.Update;

public class UserUpdateEvent : IDomainEvent, INotification
{
    public string Id {get;}

    public DateTime createdAt {get;}
    public string UserName {get;}
    public string UserId{get;}
    public UserUpdateEvent(string username , string UserId)
    {
        UserName = username;
        this.UserId = UserId;
        Id = Guid.NewGuid().ToString();
        createdAt = DateTime.UtcNow;
    }
}