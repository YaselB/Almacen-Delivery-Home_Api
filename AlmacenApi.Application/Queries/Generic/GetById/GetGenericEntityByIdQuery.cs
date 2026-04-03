using AlmacenApi.Aplication.Common.Result_Value;
using MediatR;

namespace AlmacenApi.Aplication.Queries.Generic.GetById;

public class GetGenericEntityByIdQuery<TEntity,TDto>: IRequest<Result<TDto?>>
where TDto : class
{
    public required string Id { get; set ; } 
}
