using AlmacenApi.Aplication.Features.User.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Entities.User;

namespace AlmacenApi.Aplication.Queries.User.GetById;
public class GetUserEntityByIdQuery : GetGenericEntityByIdQuery<UserEntity ,UserResultDto>{}