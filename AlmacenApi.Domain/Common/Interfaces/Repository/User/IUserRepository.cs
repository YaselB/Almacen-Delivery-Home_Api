using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Repository.Generic;

namespace AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
public interface IUserRepository : IGenericRepository<UserEntity>
{
    public Task<UserEntity?> GetUserByEmail(string username , CancellationToken cancellationToken);
}