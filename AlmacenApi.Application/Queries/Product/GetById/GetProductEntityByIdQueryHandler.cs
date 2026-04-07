using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.Product.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Common.Interfaces.Repository.Product;
using AlmacenApi.Domain.Entities.Product;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Queries.Product.GetById;

public class GetProductEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<ProductEntity, GetProductEntityByIdQuery, ProductResultDto>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    public GetProductEntityByIdQueryHandler(IProductRepository repository, IMapper mapper , ILogger<ProductEntity> logger) : base(repository, mapper)
    {
        productRepository = repository;
        this.logger = logger;
    }
    public override async Task<Result<ProductResultDto?>> Handle(GetProductEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(request.Id , cancellationToken);
        if(product == null)
        {
            logger.LogWarning("El producto con id: "+request.Id+" no se encuentra");
            return Result<ProductResultDto?>.Failure(new ProductNotFoundError());
        }
        var date = product.EndDate.OrderBy(f => f).FirstOrDefault();
        var productBack = new ProductResultDto
            {
                endDate = date,
                id = product.id,
                name = product.name,
                NameUserorAdmin = null,
                Quantity = product.Quantity,
                Unity = product.Unity,
                Category = product.Category,
                Provider = product.Provider
            };
        
        if(product.CreateByAdmin != null)
        {
            productBack.NameUserorAdmin = product.adminEntity?.Username;
        }
        if(product.CreateByUser != null)
        {
            productBack.NameUserorAdmin = product.userEntity?.UserName;
        }
        return Result<ProductResultDto?>.Success(productBack);
    }
}