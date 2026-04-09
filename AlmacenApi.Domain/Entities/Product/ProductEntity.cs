using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Domain.Entities.ProductCombo;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Events.Product.Create;
using AlmacenApi.Domain.Events.Product.Update;

namespace AlmacenApi.Domain.Entities.Product;

public class ProductEntity : GenericEntity<ProductEntity>
{
    public string name{ get; set ;} = string.Empty;
    public double Quantity { get ; set ;}
    public string Unity { get; set ; } = string.Empty;
    public string Category {get ; set ; } = string.Empty;
    public string Provider {get ; set;} = string.Empty;
    public List<DateTime> EndDate {get ; set ;} = new List<DateTime>();
    public string? CreateByUser {get ; set ;}
    public string? CreateByAdmin {get ; set ;}
    public AdminEntity? adminEntity{get; set ;}
    public UserEntity? userEntity {get; set ;}
    public string? ComboId { get ; set ; }
    public ComboEntity? Combo { get ; set ;} 
    public List<ProductComboEntity>? ProductCombos {get ; set ;}
    public ICollection<ProductOutEntity>? ProductOut {get ; set ;}

    public static ProductEntity Create(string? userId , string? AdminId , string name , int Quantity , string unity ,DateTime endDate , string Category , string Provider)
    {
        var product = new ProductEntity
        {
            name = name,
            Quantity = Quantity,
            Unity = unity,
            Category = Category,
            Provider = Provider,
        };
        product.EndDate.Add(endDate);
        if(userId != null)
        {
            product.CreateByUser = userId;
        }
        else if(AdminId != null)
        {
            product.CreateByAdmin = AdminId;
        }
        var CreateProductDomainEvent = new ProductCreateEvent(product.name , product.id);
        product.AddDomainEvent(CreateProductDomainEvent);
        return product;
    }
    public void Update(int Quantity , DateTime? endDate , string ? Admin , string ? UserId , string Provider)
    {
        this.Quantity = Quantity;
        if(endDate != null){
            this.EndDate.Add(endDate.Value);
        }
        if(Admin != null)
        {
           this.CreateByAdmin = Admin; 
           this.CreateByUser = null;
        }
        if(UserId != null)
        {
            this.CreateByUser = UserId;
            this.CreateByAdmin = null;
        }
        this.UpdatedAt = DateTime.UtcNow;
        this.Provider = Provider;
        var UpdateProductDomainEvent = new ProductUpdateEvent(this.id ,this.name);
        this.AddDomainEvent(UpdateProductDomainEvent);
    }
}
