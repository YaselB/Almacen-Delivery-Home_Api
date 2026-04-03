using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.User.Delete;

public class DeleteUserEvent : IDomainEvent, INotification
{
    public string Id{get;}

    public DateTime createdAt {get;}
    public string UserId{get;}
    public string UserName{get;}
    public DeleteUserEvent(string userId , string username)
    {
        UserId = userId;
        UserName = username;
        Id = Guid.NewGuid().ToString();
        createdAt = DateTime.Now;
    }
}