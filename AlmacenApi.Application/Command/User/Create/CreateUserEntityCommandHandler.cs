using System.Text.RegularExpressions;
using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Interfaces.Password;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.User.Create;

public class CreateUserEntityCommandHandler : CreateGenericEntityCommandHandler<UserEntity, CreateUserEntityCommand>
{
    private readonly ILogger<UserEntity> logger;
    private readonly IUserRepository userRepository;
    private readonly IPasswordHashed passwordHashed;
    private readonly IAdminRepository adminRepository;
    private readonly IHistoryRepository historyRepository;
    public CreateUserEntityCommandHandler(IUserRepository repository,ILogger<UserEntity> logger, IMapper mapper , IPasswordHashed password , IAdminRepository admin , IHistoryRepository historyRepository) : base(repository, mapper)
    {
        this.logger = logger;
        userRepository = repository;
        passwordHashed = password;
        adminRepository = admin;
        this.historyRepository = historyRepository;
    }
    public override async Task<Result<Unit>> Handle(CreateUserEntityCommand command , CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmail(command.UserName , cancellationToken);
        if(user != null)
        {
            logger.LogWarning("El usuario "+command.UserName+" ya está registrado");
            return Result<Unit>.Failure(new UserRegistered());
        }
        var admin = await adminRepository.GetByEmail(command.UserName  , cancellationToken);
        if(admin != null)
        {
            logger.LogError("Usted es administrador ,no se puede registrar como usuario con ese username: "+command.UserName);
            return Result<Unit>.Failure(new UserIfAdmin());
        }
        var hash = passwordHashed.passwordHashed(command.Password);
        var newUser = UserEntity.Create(command.FullName , command.UserName , hash);
        await userRepository.AddAsync(newUser , cancellationToken);
        var message = "Se ha creado un nuveo usuario: "+newUser.UserName;
        var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones , newUser.UserName , message);
        await historyRepository.AddAsync(history , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}