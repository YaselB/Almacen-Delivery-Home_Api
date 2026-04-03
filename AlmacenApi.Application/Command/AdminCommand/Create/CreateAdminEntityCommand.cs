using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Domain.Entities.Admin;

namespace AlmacenApi.Aplication.Command.AdminCommand.Create
{
    public class CreateAdminEntityCommand : CreateGenericEntityCommand<AdminEntity>
    {
        public string FullName {get ; set ; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}