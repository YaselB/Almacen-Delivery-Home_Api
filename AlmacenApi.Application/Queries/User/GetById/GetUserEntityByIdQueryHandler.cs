using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.User.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Queries.User.GetById;

public class GetUserEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<UserEntity, GetUserEntityByIdQuery, UserResultDto>
{
    private readonly IUserRepository userRespository;
    private readonly IMapper mapper1;
    private readonly ILogger<UserEntity> logger;
    public GetUserEntityByIdQueryHandler(IUserRepository repository, IMapper mapper, ILogger<UserEntity> logger) : base(repository, mapper)
    {
        userRespository = repository;
        mapper1 = mapper;
        this.logger = logger;
    }
    public override async Task<Result<UserResultDto?>> Handle(GetUserEntityByIdQuery command , CancellationToken cancellationToken)
    {
        var user = await userRespository.FindByIdAsync(command.Id , cancellationToken);
        if(user == null)
        {
            logger.LogWarning("No se pueden obtener los datos del usuario con id: "+command.Id);
            return Result<UserResultDto?>.Failure(new UserNotFoundError());
        }
        var userMap = mapper1.Map<UserResultDto>(user);
        return Result<UserResultDto?>.Success(userMap);
    }
}