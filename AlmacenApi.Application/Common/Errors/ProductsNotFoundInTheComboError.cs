using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class ProductsNotFoundInTheCombo : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string Error => "Algunos productos del combo no están registrados";
}