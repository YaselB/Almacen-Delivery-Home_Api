using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;
using MediatR;

namespace AlmacenApi.Aplication.Command.Generic.Update;

public class UpdateGenericEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, Result<Unit>>
where TEntity : GenericEntity<TEntity>,
new() where TCommand : UpdateGenericEntityCommand<TEntity>
{
    protected readonly IGenericRepository<TEntity> genericRepository;
    protected readonly IMapper mapper;
    public UpdateGenericEntityCommandHandler(IGenericRepository<TEntity> repository , IMapper mapper)
    {
        genericRepository = repository;
        this.mapper = mapper;
    }
    public virtual async Task<Result<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await genericRepository.FindByIdAsync(request.Id , cancellationToken);
        if(entity == null)
        {
            return Result<Unit>.Failure(new EntityNotFoundError());
        }
        mapper.Map(request , entity);
        await genericRepository.UpdateAsync(entity , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}