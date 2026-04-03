using AlmacenApi.Domain.Entities.History;

namespace AlmacenApi.Aplication.Features.History.Dto;
public class HistoryDto
{
    public required string id {get ; set ;}
    public required HistoryEntity.Type ActionType { get ; set ;}
    public required string UserOrAdminUserName { get ; set ; }
    public required string Description { get ; set ; }
    public required DateTime CreatedAt { get ; set ;}
}