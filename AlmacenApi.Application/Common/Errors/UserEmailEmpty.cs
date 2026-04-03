using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class UserEmailEmpty : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "No debe enviar un correo vacío";
}