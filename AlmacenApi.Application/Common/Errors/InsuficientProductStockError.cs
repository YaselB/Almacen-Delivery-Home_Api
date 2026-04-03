using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class InsuficientProductStockError : IError
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string Error => "El producto no cuenta con suficiente cantidad en stock para completar el pedido";
}