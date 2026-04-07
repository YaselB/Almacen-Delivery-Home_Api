using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Domain.Entities.Product;

namespace AlmacenApi.Aplication.Command.Product.Update;
public class UpdateProductEntityCommand : UpdateGenericEntityCommand<ProductEntity>
{
    public required int Quantity {get ; set ;}
    public required DateTime? endDate { get ; set ;}
    public required string? AdminId {get; set ;}
    public required string? UserId { get ; set ;}
    public required string Provider { get ; set ;}
}