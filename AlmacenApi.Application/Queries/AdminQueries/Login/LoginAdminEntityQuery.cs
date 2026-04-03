using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Entities.Admin;
using MediatR;

namespace AlmacenApi.Aplication.Queries.AdminQueries.Login;
public class LoginAdminEntityQuery : IRequest<Result<string?>>
{
    public required string UserName{get; set ;}
    public required string Password {get; set ;}
}