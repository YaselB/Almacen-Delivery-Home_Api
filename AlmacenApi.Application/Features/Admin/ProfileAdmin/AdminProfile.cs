using AlmacenApi.Aplication.Features.Dto.ResultDto;
using AlmacenApi.Domain.Entities.Admin;
using AutoMapper;

namespace AlmacenApi.Aplication.Features.ProfileAdmin;
public class AdminProfile : Profile
{
    public AdminProfile()
    {
       CreateMap<AdminEntity , ResultAdminDto>()
       .ReverseMap();
    }
}