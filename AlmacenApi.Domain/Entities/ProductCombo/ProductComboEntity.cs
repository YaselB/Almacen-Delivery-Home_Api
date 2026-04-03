using System.Net.NetworkInformation;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.Product;

namespace AlmacenApi.Domain.Entities.ProductCombo;
public class ProductComboEntity : GenericEntity<ProductComboEntity>
{
    public string ComboId { get ; set ;} = string.Empty;
    public ComboEntity ? comboEntity {get ; set ;}
    public ProductEntity ? ProductEntity {get ; set ;}
    public string ProductId { get ; set ;} = string.Empty;
    public int Quantity {get ; set ;} 
    public static ProductComboEntity Create(string ComboId , string ProductId ,int quantity)
    {
        return new ProductComboEntity
        {
            ProductId = ProductId,
            ComboId = ComboId,
            Quantity = quantity
        };
    }
    
}