using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Interfaces.Password;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Repository.Generic;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.AdminCommand.Update;

public class UpdateAdminEntityCommandHandler : UpdateGenericEntityCommandHandler<AdminEntity, UpdateAdminEntityCommand>
{
    private readonly IAdminRepository adminRepository;
    private readonly IPasswordHashed passwordHashed;
    private readonly ILogger<AdminEntity> logger;
    private readonly IHistoryRepository historyRepository;
    public UpdateAdminEntityCommandHandler(IAdminRepository repository, IMapper mapper , IPasswordHashed hashed , ILogger<AdminEntity> appLogger , IHistoryRepository history) : base(repository, mapper)
    {
        adminRepository = repository;
        passwordHashed = hashed;
        logger = appLogger;
        historyRepository = history;
    }
    public override async Task<Result<Unit>> Handle(UpdateAdminEntityCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.FindByIdAsync(request.Id , cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("Error a la hora de intentar actualizar el admin con id: "+request.Id+" ,no se encuentra dicho Admin");
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        var hashingPassword = passwordHashed.passwordHashed(request.Password);
        admin.Update(hashingPassword);
        await adminRepository.UpdateAsync(admin , cancellationToken);
        var message = "Se ha actualizado el admin: "+admin.Username;
        var history = HistoryEntity.Create(HistoryEntity.Type.Modificaciones , admin.Username ,message);
        await historyRepository.AddAsync(history , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}