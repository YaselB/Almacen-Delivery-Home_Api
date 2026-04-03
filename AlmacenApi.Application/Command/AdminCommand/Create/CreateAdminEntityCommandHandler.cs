using System.Text.RegularExpressions;
using AlmacenApi.Aplication.Command.Errors;
using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Common.Errors;
using AlmacenApi.Aplication.Common.Interfaces.Jwt;
using AlmacenApi.Aplication.Common.Result_Value;
using AlmacenApi.Aplication.Interfaces.Password;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Entities.History;
using AlmacenApi.Domain.Repository.History;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenApi.Aplication.Command.AdminCommand.Create
{
    public class CreateAdminEntityCommandHandler
        : CreateGenericEntityCommandHandler<AdminEntity, CreateAdminEntityCommand>
    {
        private readonly IAdminRepository adminRepository;
        private readonly IPasswordHashed passwordHashed;
        private readonly IUserRepository userRepository;
        private readonly IHistoryRepository historyRepository;
        private readonly ILogger<AdminEntity> logger;
        public CreateAdminEntityCommandHandler(
            IAdminRepository repository,
            IMapper mapper, IPasswordHashed password, IUserRepository user, ILogger<AdminEntity> logger, IHistoryRepository historyRepository) : base(repository, mapper)
        {
            adminRepository = repository;
            passwordHashed = password;
            userRepository = user;
            this.logger = logger;
            this.historyRepository = historyRepository;
        }
        public override async Task<Result<Unit>> Handle(CreateAdminEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await adminRepository.GetByEmail(request.UserName, cancellationToken);
            if (entity != null)
            {
                return Result<Unit>.Failure(new AdminRegisteredError());
            }
            var user = userRepository.GetUserByEmail(request.UserName, cancellationToken);
            if (user == null)
            {
                logger.LogError("Usted está registrado como usuario y no puede ser admin con ese username: " + request.UserName);
                return Result<Unit>.Failure(new AdminIfUser());
            }
            var hash = passwordHashed.passwordHashed(request.Password);
            var admin = AdminEntity.Create(request.FullName, request.UserName, hash);
            await adminRepository.AddAsync(admin, cancellationToken);
            var message = "Creando admin con nombre de usuario: "+admin.Username;
            var history = HistoryEntity.Create(HistoryEntity.Type.Creaciones, admin.Username ,message);
            await historyRepository.AddAsync(history , cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}