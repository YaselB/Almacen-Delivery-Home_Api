using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Domain.Entities.Out.ProductOut;

namespace AlmacenApi.Aplication.Command.ProductOut.Create;
public class CreateProductOutEntityCommand : CreateGenericEntityCommand<ProductOutEntity>
{
    public required string ProductId {get ; set ;} = string.Empty;
    public required int Quantity {get ; set ;}
    public required string OutMotive {get ; set ;} = string.Empty;
    public required string? AdminId {get ; set ;} = string.Empty;
    public required string? UserId {get ; set ;} = string.Empty;
    public required string? Customer { get ; set ;} = string.Empty;
}