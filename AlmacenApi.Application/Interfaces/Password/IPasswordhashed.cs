namespace AlmacenApi.Aplication.Interfaces.Password;
public interface IPasswordHashed
{
    public string passwordHashed(string password);
    public bool VerifyPassword(string password , string hash);
}