using AlmacenApi.Aplication.Interfaces.Error;
using AlmacenApi.Aplication.Interfaces.Result;

namespace AlmacenApi.Aplication.Common.Result_Without_Values;

public class Result : IResult
{
    public bool IsSuccess {get; protected set;}
    public bool IsFailure => !IsSuccess;
    public IError? error{get; protected set;}
    protected Result(bool success , IError? error)
    {
        IsSuccess = success;
        this.error = error;
    }
    public static Result Success() => new Result(true ,null);
    public static Result Failure() => new Result(false , null);
}