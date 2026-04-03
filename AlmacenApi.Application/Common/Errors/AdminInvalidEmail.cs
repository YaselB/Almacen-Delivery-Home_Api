using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class AdminInvalidEmail : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "El formato del correo no es válido";
}