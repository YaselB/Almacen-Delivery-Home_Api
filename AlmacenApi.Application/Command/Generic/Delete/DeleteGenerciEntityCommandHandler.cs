using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;
using MediatR;

namespace AlmacenApi.Aplication.Command.Generic.Delete;

public class DeleteGenericEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, Result<Unit>>
where TEntity : GenericEntity<TEntity>, 
new() where TCommand : DeleteGenericEntityCommand<TEntity>
{
    protected readonly IGenericRepository<TEntity> genericRepository;
    public DeleteGenericEntityCommandHandler(IGenericRepository<TEntity> repository ){
        genericRepository = repository;
    }
    public virtual async Task<Result<Unit>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var entity = await genericRepository.FindByIdAsync(request.Id , cancellationToken);
        if(entity == null)
        {
            return Result<Unit>.Failure(new EntityNotFoundError());
        }
        await genericRepository.RemoveAsync(entity , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}