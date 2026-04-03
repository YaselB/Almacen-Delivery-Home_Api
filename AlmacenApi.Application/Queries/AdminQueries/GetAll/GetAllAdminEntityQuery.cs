using AlmacenApi.Aplication.Features.Dto.ResultDto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Entities.Admin;

namespace AlmacenApi.Aplication.Queries.AdminQueries.GetAll;
public class GetAllAdminQuery : GetAllGenericEntityQuery<AdminEntity , ResultAdminDto>{}