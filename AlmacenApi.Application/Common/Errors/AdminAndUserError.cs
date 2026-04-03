using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class AdminAndUserError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "No pueden actualizar un usuario y un admin a la vez";
}