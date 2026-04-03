using AlmacenApi.Aplication.Features.History.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Entities.History;

namespace AlmacenApi.Aplication.Queries.History.GetAll;
public class GetAllHistoryEntityQuery : GetAllGenericEntityQuery<HistoryEntity , HistoryDto>{}