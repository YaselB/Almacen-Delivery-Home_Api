using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class ProductRegisteredError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string Error => "El producto con ese nombre ya ha sido creado";
}