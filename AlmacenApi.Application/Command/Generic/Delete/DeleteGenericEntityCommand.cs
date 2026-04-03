using AlmacenApi.Aplication.Common.Result_Value;
using MediatR;

namespace AlmacenApi.Aplication.Command.Generic.Delete;
public class DeleteGenericEntityCommand<TEntity>: IRequest<Result<Unit>>
{
    public required string Id { get ; set ;}
}