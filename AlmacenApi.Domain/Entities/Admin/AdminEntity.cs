using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Common.Permission;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.CombOut;
using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Events.AdminEvents.Create;
using AlmacenApi.Domain.Events.AdminEvents.Update;
using MediatR;

namespace AlmacenApi.Domain.Entities.Admin;
public class AdminEntity : GenericEntity<AdminEntity>
{
    public string FullName{get; set;} = string.Empty;
    public string Username {get ; set ;} = string.Empty;
    public string passwordHashed { get ; set ; } = string.Empty;
    public ICollection<ProductEntity>? Products{get; set ;}
    public ICollection<ComboEntity>? Combos { get ; set ;}
    public ICollection<ProductOutEntity>? ProductOut {get ; set;}
    public ICollection<ComboOutEntity>? ComboOut {get ; set;}
    public List<string> _permissions = new();
    public string PermissionsJson { get; set; } = "[]";

    // Propiedad de solo lectura para acceder a la lista desde fuera
    [NotMapped]
    public IReadOnlyList<string> Permission => 
        JsonSerializer.Deserialize<List<string>>(PermissionsJson) ?? new List<string>();

    // Método para agregar un permiso y actualizar el JSON
    public void AddPermission(string permission)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionsJson) ?? new List<string>();
        if (!list.Contains(permission))
        {
            list.Add(permission);
            PermissionsJson = JsonSerializer.Serialize(list);
        }
        // También actualizamos el campo interno para uso en memoria
        _permissions = list;
    }

    // Constructor estático Create actualizado
    public static AdminEntity Create(string fullName ,string username, string password)
    {
        var admin = new AdminEntity
        {
            FullName = fullName,
            Username = username,
            passwordHashed = password
        };
        var permisos = new List<string>
        {
            Permissions.CreateAdminPermission,
            Permissions.UpdateAdminPermission,
            Permissions.DeleteAdminPermission,
            Permissions.ReadAllAdminPermission,
            Permissions.ReadOneAdminPermission,
            Permissions.CreateUserPermission,
            Permissions.UpdateUserPermission,
            Permissions.DeleteUserPermission,
            Permissions.ReadAllUserPermission,
            Permissions.ReadOnlyUserPermission,
        };
        admin.PermissionsJson = JsonSerializer.Serialize(permisos);
        admin._permissions = permisos; // para uso en memoria si lo necesitas
        var createAdminEvent = new AdminCreateDomainEvents(admin.id , admin.Username);
        admin.AddDomainEvent(createAdminEvent);
        return admin;
    }

    // Método HasPermission modificado para usar la lista deserializada
    public bool HasPermission(string permission)
    {
        var list = JsonSerializer.Deserialize<List<string>>(PermissionsJson) ?? new List<string>();
        return list.Contains(permission);
    }
    public void Update(string NewPasswordHashed)
    {
       this.passwordHashed = NewPasswordHashed; 
       var updateAdminEvent = new AdminUpdateDomainEvent(this.id , this.Username);
       this.AddDomainEvent(updateAdminEvent);
    }
}