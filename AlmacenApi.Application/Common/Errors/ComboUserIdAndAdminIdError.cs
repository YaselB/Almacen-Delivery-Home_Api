using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class ComboUserAndAdminIdError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string Error => "No puede crear un combo ,sin asignarle un ususario o un admin";
}