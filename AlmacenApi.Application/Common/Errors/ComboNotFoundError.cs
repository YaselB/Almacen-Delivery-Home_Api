using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class ComboNotFoundError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "El combo no está registrado";
}