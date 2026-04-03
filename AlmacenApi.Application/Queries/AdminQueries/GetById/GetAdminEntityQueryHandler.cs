using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Features.Dto.ResultDto;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Entities.Admin;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Queries.AdminQueries.GetById;

public class GetAdminEntityQueryHandler : GetGenericEntityByIdQueryHandler<AdminEntity, GetAdminEntityById ,ResultAdminDto>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    public GetAdminEntityQueryHandler(IAdminRepository repository , ILogger<AdminEntity> appLogger ,IMapper mapper) : base(repository , mapper)
    {
        adminRepository = repository;
        logger = appLogger;
    }
    public override async Task<Result<ResultAdminDto?>> Handle(GetAdminEntityById request , CancellationToken cancellationToken)
    {
        var entity = await adminRepository.FindByIdAsync(request.Id , cancellationToken);
        logger.LogWarning(entity?.ToString());
        if(entity == null)
        {
            logger.LogWarning("El admin con identificador "+request.Id+" no se encuentra");
            return Result<ResultAdminDto?>.Failure(new AdminNotFoundError());
        }
        var adminDto = mapper.Map<ResultAdminDto>(entity);
        return Result<ResultAdminDto?>.Success(adminDto);
    }
}