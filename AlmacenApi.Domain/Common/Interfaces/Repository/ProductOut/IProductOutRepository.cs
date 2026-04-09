using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Domain.Repository.Generic;

namespace AlmacenApi.Domain.Common.Interfaces.Repository.ProductOut;
public interface IProductOutRepository : IGenericRepository<ProductOutEntity>
{
    public Task AddRange(List<ProductOutEntity> products , CancellationToken cancellationToken);
}