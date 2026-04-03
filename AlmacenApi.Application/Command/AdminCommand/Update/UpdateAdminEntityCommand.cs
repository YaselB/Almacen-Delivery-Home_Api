using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Domain.Entities.Admin;

namespace AlmacenApi.Aplication.Command.AdminCommand.Update;
public class UpdateAdminEntityCommand : UpdateGenericEntityCommand<AdminEntity>
{
    public required string Password{get ; set ;}
}