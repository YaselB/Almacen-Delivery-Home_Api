using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Domain.Entities.ProductCombo;

namespace AlmacenApi.Domain.Application.Service;
public class ComboStockService
{
    private readonly IProductRepository productRepository;

    public ComboStockService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task ValidateAndDeductStockAsync(
        IEnumerable<ProductComboEntity> productsOfCombo,
        IEnumerable<ProductEntity> products,
        CancellationToken cancellationToken)
    {
        var productDict = products.ToDictionary(p => p.id);

        // Validación
        foreach (var item in productsOfCombo)
        {
            if (!productDict.ContainsKey(item.ProductId))
                throw new Exception("Producto no encontrado");

            var product = productDict[item.ProductId];

            if (product.Quantity < item.Quantity)
                throw new Exception($"Stock insuficiente en {product.name}");
        }

        // Descuento
        foreach (var item in productsOfCombo)
        {
            var product = productDict[item.ProductId];

            product.Quantity -= item.Quantity;

            if (product.Quantity == 0)
            {
                var date = DateTime.UtcNow;
                product.EndDate = product.EndDate
                    .Where(d => d >= date)
                    .OrderBy(d => d)
                    .ToList();
            }

            await productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}