using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenApi.Infrastructure.Repository.Admin_repository;

public class AdminRepository :  GenericRepository<AdminEntity>, IAdminRepository

{
    public AdminRepository(AppDBContext context):base(context){}

    public async Task<AdminEntity?> GetByEmail(string username, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(c => c.Username == username);
        if(entity == null)
        {
            return null;
        }
        return entity;
    }
}