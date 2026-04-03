using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class UserInvalidEmail : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "El formato del correo es inválido";
}