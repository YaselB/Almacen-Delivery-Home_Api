using AlmacenApi.Aplication.Command.Combo.Update;
using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Features.ComboOut;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Entities.CombOut;

namespace AlmacenApi.Aplication.Command.ComboOut.Create;
public class CreateComboOutEntityCommand : CreateGenericEntityCommand<ComboOutEntity>
{
    public required string ComboId { get ; set ;}
    public required string OutMotive { get ; set ; }
    public required string? AdminId {get ; set ;}
    public required string ? UserId { get ; set ;}
    public required List<ComboOutDto>? ComboEntity{ get ; set ;}
    public required string? Customer { get ; set ;}
    public required DateTime ComboOutDate {get ; set;}
}