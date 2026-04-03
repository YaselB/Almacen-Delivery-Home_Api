using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Repository.History;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;

namespace AlmacenApi.Infrastructure.Repository.History;

public class HistoryRepository : GenericRepository<HistoryEntity>, IHistoryRepository
{
    public HistoryRepository(AppDBContext context) : base(context)
    {
    }
}