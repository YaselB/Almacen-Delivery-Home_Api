using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.CombOut;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Entities.ProductCombo;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Events.Combo.Create;
using AlmacenApi.Domain.Events.Combo.Update;
using AlmacenApi.Domain.Events.ComboOut;

namespace AlmacenApi.Domain.Entities.Combo;
public class ComboEntity : GenericEntity<ComboEntity>
{
    public string Name{ get ; set ;} = string.Empty;
    public List<ProductEntity>? Products {get ; set ;} = new List<ProductEntity>();
    public string? AdminId { get ; set ; }
    public AdminEntity? Admin { get ; set ;} 
    public string? UserId { get ; set ;}
    public UserEntity? User { get ; set ;}
    public List<ProductComboEntity>? ProductComboEntities{get; set ;}
    public ICollection<ComboOutEntity>? ComboOut {get ; set ;}
    public static ComboEntity Create(string name ,string? AdminId , string? UserId)
    {
        var combo = new ComboEntity
        {
            Name = name,
        };
        if(AdminId != null)
        {
            combo.AdminId = AdminId;
        }
        else
        {
            combo.UserId = UserId;
        }
        var CreateComboDomainEvent = new ComboCreateEvent(combo.id ,combo.Name);
        combo.AddDomainEvent(CreateComboDomainEvent);
        return combo;   
    }
    public void Update( string name , string? AdminId , string? UserId)
    {
        this.Name = name;
        if(AdminId != null)
        {
            this.AdminId = AdminId;
            this.UserId = null;
        }
        if(UserId != null)
        {
            this.UserId = UserId;
            this.AdminId = null;
        }
        var UpdateComboDomainEvent = new ComboUpdateEvent(this.id , this.Name);
        this.AddDomainEvent(UpdateComboDomainEvent);
    }
}