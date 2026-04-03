using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Repository.Generic;

namespace AlmacenApi.Domain.Repository.History;
public interface IHistoryRepository : IGenericRepository<HistoryEntity>{}