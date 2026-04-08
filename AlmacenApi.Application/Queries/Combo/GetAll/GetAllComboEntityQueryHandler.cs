using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Common.Interfaces.Repository.ProductComboRepository;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Entities.Combo;
using AutoMapper;

namespace AlmacenApi.Aplication.Queries.Combo.GetAll;

public class GetAllComboEntityQueryHandler : GetAllGenericEntityQueryHandler<ComboEntity, GetAllComboEntityQuery, ComboResultDto>
{
    private readonly IComboRepository comboRepository;
    private readonly IProductComboRepository productComboRepository;
    public GetAllComboEntityQueryHandler(IComboRepository repository, IMapper mapper , IProductComboRepository productCombo) : base(repository, mapper)
    {
        comboRepository = repository;
        productComboRepository = productCombo;
    }
    public override async Task<Result<IReadOnlyList<ComboResultDto>>> Handle(GetAllComboEntityQuery request, CancellationToken cancellationToken)
    {
        var combos = await comboRepository.FindALlAsync(cancellationToken);
        var combosBack = new List<ComboResultDto>();
        foreach(var i in combos)
        {
            var productComboResult = new List<ProductcomboResultDto>();
            var productCombo = await productComboRepository.GetProductsOfTheCombo(i.id ,cancellationToken);
            foreach(var j in productCombo)
            {
                var newProductComboResult = new ProductcomboResultDto
                {
                    id = j.ProductEntity?.id,
                    NameProduct = j.ProductEntity?.name,
                    Quantity = j.Quantity,
                    Unity = j.ProductEntity?.Unity
                };
                productComboResult.Add(newProductComboResult);
            }
            var newComboResult = new ComboResultDto
            {
                id = i.id,
                Name = i.Name,
                NameUserOrAdmin = null,
                Products = productComboResult
            };
            if(i.UserId != null)
            {
                newComboResult.NameUserOrAdmin = i.User?.UserName;
            }
            if(i.AdminId != null)
            {
                newComboResult.NameUserOrAdmin = i.Admin?.Username;
            }
            combosBack.Add(newComboResult);
        }
        return Result<IReadOnlyList<ComboResultDto>>.Success(combosBack);
    }
}