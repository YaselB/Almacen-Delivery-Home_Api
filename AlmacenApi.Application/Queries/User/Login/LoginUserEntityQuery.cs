using AlmacenApi.Aplication.Common.Result_Value;
using MediatR;

namespace AlmacenApi.Aplication.Queries.User.Login;
public class LoginUserEntityQuery : IRequest<Result<string?>>
{
    public required string UserName{get;set;}
    public required string Password{get;set;}
}