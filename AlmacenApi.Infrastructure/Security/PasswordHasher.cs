using AlmacenApi.Aplication.Interfaces.Password;

namespace AlmacenApi.Infrastructure.Security;

public class PasswordHashed : IPasswordHashed
{
    public string passwordHashed(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password , workFactor: 10);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password , hash);
    }
}