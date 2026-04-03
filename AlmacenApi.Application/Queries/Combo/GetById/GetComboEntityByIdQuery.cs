using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Entities.Combo;

namespace AlmacenApi.Aplication.Queries.Combo.GetById;
public class GetComboEntityByIdQuery :GetGenericEntityByIdQuery<ComboEntity , ComboResultDto>{}