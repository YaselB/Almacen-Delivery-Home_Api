using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Common.Permission;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.CombOut;
using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Events.User.Create;
using AlmacenApi.Domain.Events.User.Update;

namespace AlmacenApi.Domain.Entities.User;
public class UserEntity : GenericEntity<UserEntity>
{
    public string FullName {get; set ;} = string.Empty;
    public string UserName{get; set ;} = string.Empty;
    public string Password{get; set ;} = string.Empty;
    public ICollection<ProductEntity>? Products{get ; set ;}
    public ICollection<ComboEntity>? Combos { get ; set ;}
    public ICollection<ProductOutEntity>? ProductOut { get ; set ;}
    public ICollection<ComboOutEntity>? ComboOut { get ; set ;}
    public List<string> _permissions = new ();
    public string PermissionsJson { get; set; } = "[]";
    [NotMapped]
    public IReadOnlyList<string> Permission => 
        JsonSerializer.Deserialize<List<string>>(PermissionsJson) ?? new List<string>();

    public void AddPermission(string permission)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionsJson) ?? new List<string>();
        if (!list.Contains(permission))
        {
            list.Add(permission);
            PermissionsJson = JsonSerializer.Serialize(list);
        }
        _permissions = list;
    }

    public static UserEntity Create(string fullName,string username, string password)
    {
        var user = new UserEntity
        {
            FullName = fullName,
            UserName = username,
            Password = password
        };
        var permisos = new List<string>
        {
            Permissions.CreateUserPermission,
            Permissions.UpdateUserPermission,
            Permissions.DeleteUserPermission,
            Permissions.ReadAllUserPermission,
            Permissions.ReadOnlyUserPermission,
        };
        user.PermissionsJson = JsonSerializer.Serialize(permisos);
        var createUserDomainEvent = new UserCreateEvent(user.UserName , user.id);
        user.AddDomainEvent(createUserDomainEvent);
        Console.WriteLine(user.PermissionsJson);
        return user;
    }

    public bool HasPermission(string permission)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionsJson) ?? new List<string>();
        return list.Contains(permission);
    }
    public void Update(string password)
    {
        this.Password = password;
        var UpdateUserDomainEvent = new UserUpdateEvent(this.id , this.UserName);
        this.AddDomainEvent(UpdateUserDomainEvent);
    }
}