using AlmacenApi.Aplication.Features.Product.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Entities.Product;

namespace AlmacenApi.Aplication.Queries.Product;
public class GetAllProductEntityQuery : GetAllGenericEntityQuery<ProductEntity , ProductResultDto>
{
    
}