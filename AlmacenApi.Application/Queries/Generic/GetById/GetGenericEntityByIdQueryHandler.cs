using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;
using MediatR;

namespace AlmacenApi.Aplication.Queries.Generic.GetById;

public class GetGenericEntityByIdQueryHandler<TEntity, TQuery , TDto> : IRequestHandler<TQuery, Result<TDto?>>
where TEntity : GenericEntity<TEntity>,
new() where TQuery : GetGenericEntityByIdQuery<TEntity ,TDto>
where TDto : class
{
    protected readonly IGenericRepository<TEntity> genericRepository;
    protected readonly IMapper mapper ;
    public GetGenericEntityByIdQueryHandler(IGenericRepository<TEntity> repository ,IMapper mapper)
    {
        genericRepository = repository;
        this.mapper = mapper;
    }
    public virtual async Task<Result<TDto?>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var entity = await genericRepository.FindByIdAsync(request.Id , cancellationToken);
        if(entity == null)
        {
            return Result<TDto?>.Failure(new EntityNotFoundError());
        }
        var Dto = mapper.Map<TDto>(entity);
        return Result<TDto?>.Success(Dto);
    }
}