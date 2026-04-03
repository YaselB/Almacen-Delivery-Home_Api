using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Repository.Generic;

namespace AlmacenApi.Domain.Common.Interfaces.Repository.Product;
public interface IProductRepository : IGenericRepository<ProductEntity>
{
    public Task<IReadOnlyList<ProductEntity>> GetProductByAdmin(string AdminId, CancellationToken cancellationToken);
    public Task<IReadOnlyList<ProductEntity>> GetProductByUser(string UserId , CancellationToken cancellationToken);
    public Task RemoveUserOrAdminId(string? UserId , string? AdminId ,string productId, CancellationToken cancellationToken);
    public Task<List<ProductEntity>> GetProductsByIds(List<string> ids , CancellationToken cancellationToken);
    public Task<ProductEntity?> GetProductByName(string name , CancellationToken cancellationToken);
}