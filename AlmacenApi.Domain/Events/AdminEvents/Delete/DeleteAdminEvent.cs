using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.AdminEvents.Delete;

public class DeleteAdminDomainEvent : IDomainEvent, INotification
{
    public string Id {get;}
    public DateTime createdAt {get;}
    public string AdminId {get;}
    public string UserName {get;}
    public DeleteAdminDomainEvent(string adminId , string username)
    {
        Id = Guid.NewGuid().ToString();
        createdAt = DateTime.UtcNow;
        AdminId = adminId;
        UserName = username;
    }
}