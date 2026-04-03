using AlmacenApi.Aplication.Common.Result_Without_Values;
using AlmacenApi.Aplication.Interfaces.Error;

namespace AlmacenApi.Aplication.Common.Result_Value;

public class Result<T> : Result
{
    public T? Value {get; private set;}
    protected Result(bool success,T? value, IError? error) : base(success, error)
    {
        Value = value;
    }
    public static Result<T> Success(T value)
    {
        return new Result<T>(true , value  , null);
    }
    public static Result<T> Failure(IError error)
    {
        return new Result<T>(false , default , error);
    }
}