using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common;
using MediatR;

namespace AlmacenApi.Aplication.Command.Generic.Create;
public abstract class CreateGenericEntityCommand<TEntity> : IRequest<Result<Unit>>
where TEntity : class
{

}
