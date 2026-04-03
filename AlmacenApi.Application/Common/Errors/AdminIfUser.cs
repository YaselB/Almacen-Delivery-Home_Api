using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class AdminIfUser : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "Usted no puede registrarse con ese username como administrador porque ya está registrado como usuario";
}