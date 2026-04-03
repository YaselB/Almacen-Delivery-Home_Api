using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenApi.Infrastructure.Repository.Product;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
    private readonly AppDBContext context1;
    public ProductRepository(AppDBContext context) : base(context)
    {
        context1 = context;
    }
    public async override Task<ProductEntity?> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        var product = await context1.Products.Include(p => p.adminEntity).Include(p => p.userEntity).FirstOrDefaultAsync(option => option.id == id);
        if(product == null)
        {
            return null;
        }
        return product;
    }
    public async override Task<IReadOnlyList<ProductEntity>> FindALlAsync(CancellationToken cancellationToken)
    {
        var products = await context1.Products.Include(p => p.adminEntity).Include(p => p.userEntity).ToListAsync();
        return products;
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductByAdmin(string AdminId, CancellationToken cancellationToken)
    {
        var product = await context1.Products.Where( p => p.CreateByAdmin == AdminId).ToListAsync();
        return product;
    }

    public async Task<IReadOnlyList<ProductEntity>> GetProductByUser(string UserId, CancellationToken cancellationToken)
    {
        var product = await context1.Products.Where(p => p.CreateByUser == UserId).ToListAsync();
        return product;
    }

    public async Task RemoveUserOrAdminId(string? UserId, string? AdminId,string productId , CancellationToken cancellationToken)
    {
        var product = await context1.Products.FirstOrDefaultAsync(p => p.id == productId);
        if(product == null)
        {
            return;
        }
        if(UserId == null)
        {
            product.CreateByAdmin = null;
        }
        else
        {
            product.CreateByUser = null;
        }
        await UpdateAsync(product , cancellationToken);
        return;
    }

    public async Task<List<ProductEntity>> GetProductsByIds(List<string> ids, CancellationToken cancellationToken)
    {
        return await context1.Products.Where(p => ids.Contains(p.id)).ToListAsync();
    }

    public async Task<ProductEntity?> GetProductByName(string name, CancellationToken cancellationToken)
    {
        var product = await context1.Products.FirstOrDefaultAsync( P => P.name ==  name);
        if(product == null)
        {
            return null;
        }
        return product;
    }
}