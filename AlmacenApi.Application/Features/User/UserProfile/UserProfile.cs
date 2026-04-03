using AlmacenApi.Aplication.Features.User.Dto;
using AlmacenApi.Domain.Entities.User;
using AutoMapper;

namespace AlmacenApi.Aplication.Features.User.UserProfile;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity , UserResultDto>()
        .ReverseMap();
    }
}