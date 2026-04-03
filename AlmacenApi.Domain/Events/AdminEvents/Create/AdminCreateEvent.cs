using AlmacenApi.Domain.Interfaces.IDomainEvent;
using MediatR;

namespace AlmacenApi.Domain.Events.AdminEvents.Create;

public class AdminCreateDomainEvents : IDomainEvent , INotification
{

    public string AdminId{ get;}
    public string Username{ get ;}

    public string Id {get;}

    public DateTime createdAt {get;}

    public AdminCreateDomainEvents(string AdminId , string username)
    {
        this.AdminId = AdminId;
        this.Username = username;
        Id = Guid.NewGuid().ToString();
        createdAt = DateTime.UtcNow;
    }
}