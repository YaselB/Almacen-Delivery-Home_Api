using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Interfaces.Password;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.User.Update;

public class UpdateUserEntityCommandHandler : UpdateGenericEntityCommandHandler<UserEntity, UpdateUserEntityCommand>
{
    private readonly IUserRepository userRepository;
    private readonly ILogger<UserEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    private readonly IHistoryRepository historyRepository;
    public UpdateUserEntityCommandHandler(IUserRepository repository, IMapper mapper , ILogger<UserEntity> logger , IPasswordHashed passwordHashed , IHistoryRepository historyRepository) : base(repository, mapper)
    {
        userRepository = repository;
        this.logger = logger;
        this.passwordHashed = passwordHashed;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(UpdateUserEntityCommand command , CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.Id , cancellationToken);
        if(user == null)
        {
            logger.LogWarning("El usuario con id: "+command.Id+" no se encuentra");
            return Result<Unit>.Failure(new UserNotFoundError());
        }
        var hash = passwordHashed.passwordHashed(command.password);
        user.Update(hash);
        await userRepository.UpdateAsync(user , cancellationToken);
        var message = "Se ha actualizado el usuario: "+user.UserName;
        var history = HistoryEntity.Create(HistoryEntity.Type.Modificaciones , user.UserName ,message , null);
        await historyRepository.AddAsync(history , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}