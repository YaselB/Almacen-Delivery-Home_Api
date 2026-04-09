using AlmacenApi.Aplication.Interfaces.Error;
using Microsoft.AspNetCore.Http;

namespace AlmacenApi.Aplication.Common.Errors;

public class InsuficientStockOfProductT : IError
{
    public string ProductName { get ; set ;} = string.Empty;
    public InsuficientStockOfProductT(string name)
    {
        this.ProductName = name;
    }
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string Error => "El producto: "+this.ProductName+" no se le puede dar salida ,porque no hay suficiente cantidad en el almacen";
}