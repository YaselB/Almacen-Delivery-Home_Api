using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class UserRegistered : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "Ese correo ya esta registrado";
}