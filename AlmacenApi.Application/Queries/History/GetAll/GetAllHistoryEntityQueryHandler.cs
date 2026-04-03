using AlmacenApi.Aplication.Features.History.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;

namespace AlmacenApi.Aplication.Queries.History.GetAll;

public class GetAllHistoryEntityQueryHandler : GetAllGenericEntityQueryHandler<HistoryEntity, GetAllHistoryEntityQuery, HistoryDto>
{
    public GetAllHistoryEntityQueryHandler(IHistoryRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}