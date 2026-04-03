using AlmacenApi.Aplication.Interfaces.Error;

namespace AlmacenApi.Aplication.Interfaces.Result;
public interface IResult
{
    public bool IsSuccess{get;}
    public bool IsFailure{get;}
    public IError? error{get;}
}