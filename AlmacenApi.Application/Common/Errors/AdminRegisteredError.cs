using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Command.Errors;

public class AdminRegisteredError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "Ese administrador ya está registrado";
}