using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Common.Interfaces.Repository.ProductComboRepository;
using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Queries.Combo.GetById;

public class GetComboEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<ComboEntity, GetComboEntityByIdQuery, ComboResultDto>
{
    private readonly IComboRepository comboRepository;
    private readonly IProductComboRepository productComboRepository;
    private readonly ILogger<ComboEntity> logger;
    public GetComboEntityByIdQueryHandler(IComboRepository repository, IMapper mapper , ILogger<ComboEntity> logger , IProductComboRepository productCombo) : base(repository, mapper)
    {
        comboRepository = repository;
        this.logger = logger;
        productComboRepository = productCombo;
    }
    public override async Task<Result<ComboResultDto?>> Handle(GetComboEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.FindByIdAsync(request.Id , cancellationToken);
        if(combo == null)
        {
            logger.LogWarning("El combo con id: "+request.Id+" no se encuentra");
            return Result<ComboResultDto?>.Failure(new ComboNotFoundError());
        }
        var productcombos = new List<ProductcomboResultDto>();
        var productCombosEntity = await productComboRepository.GetProductsOfTheCombo(combo.id, cancellationToken);
        foreach(var i in productCombosEntity)
        {
            var newProductComboResult = new ProductcomboResultDto
            {
                id = i.ProductEntity?.id,
                NameProduct = i.ProductEntity?.name,
                Quantity = i.Quantity,
                Unity = i.ProductEntity?.Unity
            };
            productcombos.Add(newProductComboResult);
        }
        var comboBack = new ComboResultDto
        {
            id = combo.id,
            Name = combo.Name,
            NameUserOrAdmin = null,
            Products = productcombos
        };
        if(combo.UserId != null)
        {
            comboBack.NameUserOrAdmin = combo.User?.UserName;
        }
        if(combo.AdminId != null)
        {
            comboBack.NameUserOrAdmin = combo.Admin?.Username;
        }
        return Result<ComboResultDto?>.Success(comboBack);
    }
}