using AlmacenApi.Domain.Common.Interfaces.Repository.ProductOut;
using AlmacenApi.Domain.Entities.Out.ProductOut;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;

namespace AlmacenApi.Infrastructure.Repository.ProductOut;

public class ProductOutRepository : GenericRepository<ProductOutEntity>, IProductOutRepository
{
    public ProductOutRepository(AppDBContext context) : base(context)
    {
    }
}