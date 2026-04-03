using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class UserPasswordMatchError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "La contraseña es inválida";
}