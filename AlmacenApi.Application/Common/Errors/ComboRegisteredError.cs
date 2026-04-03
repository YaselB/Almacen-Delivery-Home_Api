using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class ComboRegisteredError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string Error => "Ese combo ya está creado";
}