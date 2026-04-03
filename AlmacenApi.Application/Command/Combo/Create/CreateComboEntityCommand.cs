using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.Product;

namespace AlmacenApi.Aplication.Command.Combo.Create;
public class CreateComboEntityCommand : CreateGenericEntityCommand<ComboEntity>
{
    public string Name { get; set ;} = string.Empty;
    public List<ProductComboDto> products {get ; set ;} = new List<ProductComboDto>();
    public string? AdminId { get ; set ;} = string.Empty;
    public string? UserId { get ; set ;} = string.Empty;

}