using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class AdminUsernameEmpty : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "No debe dejar el username vacío";
}