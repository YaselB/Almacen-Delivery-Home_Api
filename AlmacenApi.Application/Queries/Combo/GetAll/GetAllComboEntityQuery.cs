using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Entities.Combo;

namespace AlmacenApi.Aplication.Queries.Combo.GetAll;
public class GetAllComboEntityQuery : GetAllGenericEntityQuery<ComboEntity, ComboResultDto>{}