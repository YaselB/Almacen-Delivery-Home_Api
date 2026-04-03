using AlmacenApi.Aplication.Features.Dto.ResultDto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Entities.Admin;

namespace AlmacenApi.Aplication.Queries.AdminQueries.GetById;
public class GetAdminEntityById : GetGenericEntityByIdQuery<AdminEntity , ResultAdminDto>{}