using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Events.ProductOut.Create;

namespace AlmacenApi.Domain.Entities.Out.ProductOut;
public class ProductOutEntity : GenericEntity<ProductOutEntity>
{
    public string ProductName {get ; set ;} = string.Empty;
    public int Quantity {get ; set ;}
    public string OutMotive {get ; set ;} = string.Empty;
    public string? AdminId {get; set;}
    public string? UserId {get; set ;}
    public string ProductId {get ; set ; } = string.Empty;
    public string? Customer { get ; set ;}
    public UserEntity? User {get ; set ;}
    public AdminEntity? Admin {get ; set ;}
    public ProductEntity? Product {get ; set ;}
    public static ProductOutEntity Create(string? UserId , string? AdminId , string ProductName ,int Quantity , string OutMotive , string ProductId , string? Customer)
    {
        var productOutEntity = new ProductOutEntity
        {
            ProductName = ProductName,
            ProductId = ProductId,
            OutMotive = OutMotive,
            Quantity = Quantity,
            Customer = Customer
        };
        if(UserId != null)
        {
            productOutEntity.UserId = UserId;
        }
        if(AdminId != null)
        {
            productOutEntity.AdminId = AdminId;
        }
        var CreateProductOutDomainEvent = new ProductOutCreate(ProductName , productOutEntity.id);
        return productOutEntity;
    }

}