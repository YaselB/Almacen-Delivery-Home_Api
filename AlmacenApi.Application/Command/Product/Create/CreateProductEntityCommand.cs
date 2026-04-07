using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Domain.Entities.Product;

namespace AlmacenApi.Aplication.Command.Product.Create;
public class CreateProductEntityCommand : CreateGenericEntityCommand<ProductEntity>
{
    public string ProductName {get; set ;} = string.Empty;
    public int Quantity {get; set ;} = 0;
    public string Unity { get ; set ;} = string.Empty;
    public DateTime endDate { get ; set ;} = new DateTime();
    public string Provider {get ; set ;} = string.Empty;
    public string? AdminId {get ; set ;} = string.Empty;
    public string? UserId {get; set ;} = string.Empty;
    public string Category { get ; set ;} = string .Empty;
}