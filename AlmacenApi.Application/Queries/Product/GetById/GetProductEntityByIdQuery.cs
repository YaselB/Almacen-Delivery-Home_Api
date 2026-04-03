using AlmacenApi.Aplication.Features.Product.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Entities.Product;

namespace AlmacenApi.Aplication.Queries.Product.GetById;
public class GetProductEntityByIdQuery : GetGenericEntityByIdQuery<ProductEntity, ProductResultDto>
{
    
}