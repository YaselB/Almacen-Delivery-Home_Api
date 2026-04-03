using AlmacenApi.Aplication.Common.Result_Value;
using MediatR;

namespace AlmacenApi.Aplication.Queries.Generic.GetAll;
public class GetAllGenericEntityQuery<TEntity ,TDto> : IRequest<Result<IReadOnlyList<TDto>>>
where TDto : class
{}