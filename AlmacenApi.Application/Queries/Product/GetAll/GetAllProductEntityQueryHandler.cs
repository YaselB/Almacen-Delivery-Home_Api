using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.Product.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.Product;
using AutoMapper;

namespace AlmacenApi.Aplication.Queries.Product.GetAll;

public class GetAllProductEntityQueryHandler : GetAllGenericEntityQueryHandler<ProductEntity, GetAllProductEntityQuery, ProductResultDto>
{
    private readonly IProductRepository productRepository;
    public GetAllProductEntityQueryHandler(IProductRepository repository, IMapper mapper) : base(repository, mapper)
    {
        productRepository = repository;
    }
    public override async Task<Result<IReadOnlyList<ProductResultDto>>> Handle(GetAllProductEntityQuery query , CancellationToken cancellationToken)
    {
        var product = await productRepository.FindALlAsync(cancellationToken);
        var productList = new List<ProductResultDto>();
        foreach( var i in product)
        {
            var date = i.EndDate.OrderBy(f => f).FirstOrDefault();
            var productBack = new ProductResultDto
            {
                    endDate = date,
                    id = i.id,
                    name = i.name,
                    NameUserorAdmin = null,
                    Quantity = i.Quantity,
                    Unity = i.Unity,
                    Category = i.Category,
                    Provider = i.Provider
            };
            if(i.CreateByUser != null)
            {
                productBack.NameUserorAdmin = i.userEntity?.UserName;
            }
            if(i.CreateByAdmin != null)
            {
                productBack.NameUserorAdmin = i.adminEntity?.Username;
            }
            productList.Add(productBack);
        }
        return Result<IReadOnlyList<ProductResultDto>>.Success(productList);
    }
}
