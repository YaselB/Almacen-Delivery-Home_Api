using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class AdminNotFoundError : IError
{
    public int StatusCode => StatusCodes.Status404NotFound;

    public string Error => "Admin no encontrado";
}