using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class UserNotFoundError : IError
{
    public int StatusCode => StatusCodes.Status404NotFound;

    public string Error => "Ese usuario no está registrado";
}