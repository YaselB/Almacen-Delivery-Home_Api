using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Interfaces.Jwt;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Interfaces.Password;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Queries.User.Login;

public class LoginUserEntityQueryHandler : IRequestHandler<LoginUserEntityQuery, Result<string?>>{
    private readonly IUserRepository userRepository;
    private readonly ILogger<UserEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    private readonly IJwtGenerator jwtGenerator;
    public LoginUserEntityQueryHandler(IUserRepository repository , ILogger<UserEntity> logger , IPasswordHashed passwordHashed , IJwtGenerator jwtGenerator)
    {
        userRepository = repository;
        this.logger = logger;
        this.passwordHashed = passwordHashed;
        this.jwtGenerator = jwtGenerator;
    }
    public async Task<Result<string?>> Handle(LoginUserEntityQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmail(request.UserName, cancellationToken);
        if(user == null)
        {
            logger.LogWarning("El usuario con username: "+request.UserName+" no existe");
            return Result<string?>.Failure(new UserNotFoundError());
        }
        if(!passwordHashed.VerifyPassword(request.Password , user.Password))
        {
          logger.LogWarning("El usuario: "+request.UserName+" intento entrar con una contraseña inválida");
          return Result<string?>.Failure(new UserPasswordMatchError());  
        }
        var token = jwtGenerator.GenerateUserToken(user);
        return Result<string?>.Success(token);
    }
}