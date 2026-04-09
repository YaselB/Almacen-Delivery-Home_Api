using AlmacenApi.Domain.Common.Interfaces.Repository.ProductOut;
using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;

namespace AlmacenApi.Infrastructure.Repository.ProductOut;

public class ProductOutRepository : GenericRepository<ProductOutEntity>, IProductOutRepository
{
    private readonly AppDBContext dBContext;
    public ProductOutRepository(AppDBContext context) : base(context)
    {
        dBContext = context;
    }

    public async Task AddRange(List<ProductOutEntity> products, CancellationToken cancellationToken)
    {
        await dBContext.ProductOut.AddRangeAsync(products , cancellationToken);
        await dBContext.SaveChangesAsync(cancellationToken);
    }
}