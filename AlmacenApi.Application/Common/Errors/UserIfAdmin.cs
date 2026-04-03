using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class UserIfAdmin : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string Error => "Usted es administrador y no se puede registrar con ese username como usuario";
}