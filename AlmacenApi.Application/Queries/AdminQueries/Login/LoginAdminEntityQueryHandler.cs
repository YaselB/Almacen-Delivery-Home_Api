using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Interfaces.Jwt;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Interfaces.Password;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Entities.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Queries.AdminQueries.Login;

public class LoginAdminEntityQueryHanlder : IRequestHandler<LoginAdminEntityQuery, Result<string?>>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    private readonly IJwtGenerator jwtGenerator;
    public LoginAdminEntityQueryHanlder(IAdminRepository repository , ILogger<AdminEntity> logger , IPasswordHashed password , IJwtGenerator generator)
    {
        adminRepository = repository;
        this.logger = logger;
        passwordHashed = password;
        jwtGenerator = generator;
    }
    public async Task<Result<string?>> Handle(LoginAdminEntityQuery request, CancellationToken cancellationToken)
    {
        var entity = await adminRepository.GetByEmail(request.UserName , cancellationToken);
        if(entity == null)
        {
            logger.LogWarning("El usuario con email : "+request.UserName+" no está registrado");
            return Result<string?>.Failure(new AdminNotFoundError());
        }
        if(!passwordHashed.VerifyPassword(request.Password , entity.passwordHashed))
        {
            logger.LogWarning("Esa contraseña no es correcta ,por favor corrija esa contraseña");
            return Result<string?>.Failure(new AdminPasswordMatchError());
        }
        var token = jwtGenerator.GenerateToken(entity);
        return Result<string?>.Success(token);
    }
}