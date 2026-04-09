using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Events.User.Delete;
using AlmacenApi.Domain.Repository.Generic;
using AlmacenApi.Domain.Repository.History;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.User.Delete;

public class DeleteUserEntityCommandHandler : DeleteGenericEntityCommandHandler<UserEntity, DeleteUserEntityCommand>
{
    private readonly IUserRepository userRepository;
    private readonly ILogger<UserEntity> logger;
    private readonly IProductRepository productRepository;
    private readonly IComboRepository comboRepository;
    private readonly IHistoryRepository historyRepository;
    public DeleteUserEntityCommandHandler(IUserRepository repository , ILogger<UserEntity> logger , IProductRepository product , IComboRepository combo , IHistoryRepository historyRepository) : base(repository)
    {
        this.userRepository = repository;
        this.logger = logger;
        productRepository = product;
        comboRepository = combo;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(DeleteUserEntityCommand command , CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.Id , cancellationToken);
        if(user == null)
        {
            logger.LogWarning("El usuario con Id: "+command.Id+" no existe ,por lo que no se puede eliminar");
            return Result<Unit>.Failure(new UserNotFoundError());
        }
        var product = await productRepository.GetProductByUser(user.id , cancellationToken);
        foreach(var i in product)
        {
            await productRepository.RemoveUserOrAdminId(i.CreateByUser , null ,i.id , cancellationToken);
            logger.LogInformation("El producto con id: "+i.id+" se le ha removido el id del usuario");
        }
        var combo = await comboRepository.GetComboyUser(user.id , cancellationToken);
        foreach(var i in combo)
        {
            await comboRepository.RemoveUserAndAdmin(i.UserId ,null , i.id ,cancellationToken);
            logger.LogInformation("El combo con id: "+i.id+" se le ha removido el id del usuario");
        }
        var DeleteUserDomainEvent = new DeleteUserEvent(user.id , user.UserName);
        user.AddDomainEvent(DeleteUserDomainEvent);
        await userRepository.RemoveAsync(user , cancellationToken);
        var message = "Se ha eliminado el usuario: "+user.UserName;
        var history = HistoryEntity.Create(HistoryEntity.Type.Eliminaciones , user.UserName ,message , null);
        await historyRepository.AddAsync(history ,cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}