using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Domain.Entities.User;

namespace AlmacenApi.Aplication.Command.User.Create;
public class CreateUserEntityCommand : CreateGenericEntityCommand<UserEntity>
{
    public string FullName {get; set ;} = string.Empty;
    public string UserName {get; set ;} = string.Empty;
    public string Password {get; set ; } = string.Empty;
}