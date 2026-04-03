using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;
using MediatR;

namespace AlmacenApi.Aplication.Command.Generic.Create;

public abstract class CreateGenericEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, Result<Unit>>
where TEntity : GenericEntity<TEntity>, new() 
where TCommand : CreateGenericEntityCommand<TEntity>  

{
    protected readonly IGenericRepository<TEntity> genericRepository;
    protected readonly IMapper mapper;

    public CreateGenericEntityCommandHandler(IGenericRepository<TEntity> repository , IMapper mapper)
    {
        genericRepository = repository;
        this.mapper = mapper;
    }

    public virtual async Task<Result<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = new TEntity();
        mapper.Map(request , entity);
        await genericRepository.AddAsync(entity , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}