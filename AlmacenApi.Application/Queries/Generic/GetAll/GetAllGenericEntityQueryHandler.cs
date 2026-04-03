using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;
using MediatR;

namespace AlmacenApi.Aplication.Queries.Generic.GetAll;

public class GetAllGenericEntityQueryHandler<TEntity, TQuery , TDto> : IRequestHandler<TQuery, Result<IReadOnlyList<TDto>>>
where TEntity : GenericEntity<TEntity>,
new() where TQuery : GetAllGenericEntityQuery<TEntity, TDto>
where TDto : class
{
    protected readonly IGenericRepository<TEntity> genericRepository;
    protected readonly IMapper mapper;
    public GetAllGenericEntityQueryHandler(IGenericRepository<TEntity> repository , IMapper mapper)
    {
        genericRepository = repository;
        this.mapper = mapper;
    }
    public virtual async Task<Result<IReadOnlyList<TDto>>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var entities = await genericRepository.FindALlAsync(cancellationToken);
        var dtos = new List<TDto>();
        foreach(var i in entities)
        {
            dtos.Add(mapper.Map<TDto>(i));
        }
        return Result<IReadOnlyList<TDto>>.Success(dtos);
    }
}