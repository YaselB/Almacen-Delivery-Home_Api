using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Domain.Entities.Combo;

namespace AlmacenApi.Aplication.Command.Combo.Update;
public class UpdateComboEntityCommand : UpdateGenericEntityCommand<ComboEntity>
{
    public required string Name {get ; set;}
    public required List<ProductComboDto> ProductsIds { get; set ;}
    public required string? AdminId { get ; set;}
    public required string? UserId {get ; set ;}
}