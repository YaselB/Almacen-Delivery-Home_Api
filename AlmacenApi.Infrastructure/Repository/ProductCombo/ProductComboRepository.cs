using AlmacenApi.Common.Interfaces.Repository.ProductComboRepository;
using AlmacenApi.Domain.Entities.ProductCombo;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenApi.Infrastructure.Repository.ProductCombo;

public class ProductComboRepository : GenericRepository<ProductComboEntity> , IProductComboRepository
{
    private readonly AppDBContext dbContext;
    public ProductComboRepository(AppDBContext context) : base(context)
    {
        dbContext = context;
    }

    public async Task AddRange(List<ProductComboEntity> products, CancellationToken cancellationToken)
    {
        dbContext.ProductComboEntity.AddRange(products);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IList<ProductComboEntity>> GetProductsOfTheCombo(string ComboId, CancellationToken cancellationToken)
    {
        var productsCombos = await dbContext.ProductComboEntity.Include(p => p.ProductEntity).Where(p => p.ComboId == ComboId).ToListAsync();
        return productsCombos;
    }
}