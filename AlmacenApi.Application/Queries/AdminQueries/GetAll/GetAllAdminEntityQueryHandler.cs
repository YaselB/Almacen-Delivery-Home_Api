using AlmacenApi.Aplication.Features.Dto.ResultDto;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Repository.Generic;
using AutoMapper;

namespace AlmacenApi.Aplication.Queries.AdminQueries.GetAll;

public class GetAllAdminEntityQueryHandler : GetAllGenericEntityQueryHandler<AdminEntity, GetAllAdminQuery , ResultAdminDto>
{
    
    public GetAllAdminEntityQueryHandler(IGenericRepository<AdminEntity> repository, IMapper mapper) : base(repository , mapper)
    {
    }
}