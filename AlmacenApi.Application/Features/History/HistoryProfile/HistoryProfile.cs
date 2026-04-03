using AlmacenApi.Aplication.Features.History.Dto;
using AlmacenApi.Domain.Entities.History;
using AutoMapper;

namespace AlmacenApi.Aplication.Features.History.HistoryProfile;
public class HistoryProfile : Profile
{
    public HistoryProfile()
    {
        CreateMap<HistoryDto ,HistoryEntity>()
        .ReverseMap();
    }
}