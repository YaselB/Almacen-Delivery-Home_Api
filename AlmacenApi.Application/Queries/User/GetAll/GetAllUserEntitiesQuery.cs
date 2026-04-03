using AlmacenApi.Aplication.Features.User.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;

namespace AlmacenApi.Aplication.Queries.User.GetAll;

public class GetAllUserEntitiesQuery : GetAllGenericEntityQuery<UserEntity , UserResultDto>{}