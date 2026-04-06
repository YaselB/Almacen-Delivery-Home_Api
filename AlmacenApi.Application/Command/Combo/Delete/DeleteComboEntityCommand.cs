using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Domain.Entities.Combo;

namespace AlmacenApi.Aplication.Command.Combo.Delete;
public class DeleteComboEntityCommand : DeleteGenericEntityCommand<ComboEntity>
{
    public required string? UserId { get ; set ;}
    public required string? AdminId { get ; set ;}
}