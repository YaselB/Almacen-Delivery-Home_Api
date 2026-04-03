using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Domain.Entities.User;

namespace AlmacenApi.Aplication.Command.User.Update;
public class UpdateUserEntityCommand : UpdateGenericEntityCommand<UserEntity>
{
    public required string password{get; set ;}
}