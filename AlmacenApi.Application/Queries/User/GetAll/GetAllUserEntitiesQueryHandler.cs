using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.User.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;

namespace AlmacenApi.Aplication.Queries.User.GetAll;

public class GetAllEntitiesQueryHandler : GetAllGenericEntityQueryHandler<UserEntity, GetAllUserEntitiesQuery, UserResultDto>
{
    public GetAllEntitiesQueryHandler(IUserRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}