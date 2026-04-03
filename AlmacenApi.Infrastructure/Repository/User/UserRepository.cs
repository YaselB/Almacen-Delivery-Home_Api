using AlmacenApi.Domain.Common.Interfaces.Repository.UserRepository;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Domain.Repository.Generic;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenApi.Infrastructure.Repository.UserRespository;

public class UserRepository :  GenericRepository<UserEntity> ,IUserRepository
{
    public UserRepository(AppDBContext context) : base(context)
    {
    }

    public async Task<UserEntity?> GetUserByEmail(string username , CancellationToken cancellationToken)
    {
        var user = await _dbSet.FirstOrDefaultAsync(option => option.UserName == username);
        if(user == null)
        {
            return null;
        }
        return user ;
    }
}