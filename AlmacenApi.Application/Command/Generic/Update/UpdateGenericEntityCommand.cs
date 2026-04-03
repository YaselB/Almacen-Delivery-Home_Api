using AlmacenApi.Aplication.Common.Result_Value;
using MediatR;

namespace AlmacenApi.Aplication.Command.Generic.Update;
public class UpdateGenericEntityCommand<TEntity> : IRequest<Result<Unit>>
{
    public required string Id{get; set;}
}