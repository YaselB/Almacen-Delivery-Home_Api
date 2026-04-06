using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Domain.Entities.Product;

namespace AlmacenApi.Aplication.Command.Product.Delete;
public class DeleteProductEntityCommand : DeleteGenericEntityCommand<ProductEntity>
{
    public required string? UserId {get ; set;}
    public required string? AdminId {get ; set ;}
}