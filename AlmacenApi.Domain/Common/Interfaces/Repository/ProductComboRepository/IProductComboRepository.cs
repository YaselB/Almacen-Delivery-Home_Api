using AlmacenApi.Domain.Entities.ProductCombo;
using AlmacenApi.Domain.Repository.Generic;

namespace AlmacenApi.Common.Interfaces.Repository.ProductComboRepository;
public interface IProductComboRepository : IGenericRepository<ProductComboEntity>
{
    public Task<IList<ProductComboEntity>> GetProductsOfTheCombo(string ComboId , CancellationToken cancellationToken);
    public Task AddRange(List<ProductComboEntity> products , CancellationToken cancellationToken);
}