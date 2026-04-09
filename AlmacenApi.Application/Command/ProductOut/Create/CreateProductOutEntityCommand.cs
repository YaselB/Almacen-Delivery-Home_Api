using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Domain.Entities.Out.ProductOut;

namespace AlmacenApi.Aplication.Command.ProductOut.Create;
public class CreateProductOutEntityCommand : CreateGenericEntityCommand<ProductOutEntity>
{
    public required List<ProductComboDto> Products {get ; set ;} 
    public required DateTime ProductOutDate {get ; set ;}
    public required string OutMotive {get ; set ;} = string.Empty;
    public required string? AdminId {get ; set ;} = string.Empty;
    public required string? UserId {get ; set ;} = string.Empty;
    public required string? Customer { get ; set ;} = string.Empty;
}