using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.User;

namespace AlmacenApi.Aplication.Common.Interfaces.Jwt;
public interface IJwtGenerator
{
    public string GenerateToken(AdminEntity admin);
    public string GenerateUserToken (UserEntity user);
}