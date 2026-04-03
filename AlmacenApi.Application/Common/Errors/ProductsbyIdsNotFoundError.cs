using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class ProductsByIdsNotFoundError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string Error => "Algunos productos no fueron encontrados";
}