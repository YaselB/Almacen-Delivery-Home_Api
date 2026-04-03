using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.AdminEvents.Update;

public class AdminUpdateDomainEvent : IDomainEvent , INotification
{
    public string Id {get;}
    public DateTime createdAt {get;}
    public string AdminId {get;}
    public string Username{get;}
    public AdminUpdateDomainEvent(string adminId , string username)
    {
        Id = Guid.NewGuid().ToString();
        createdAt = DateTime.UtcNow;
        AdminId = adminId;
        Username = username;
    }
}