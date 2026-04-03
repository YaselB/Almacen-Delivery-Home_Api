using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class ProductNotFoundError : IError
{
    public int StatusCode => StatusCodes.Status404NotFound;

    public string Error => "Producto no encontrado";
}