using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Domain.Repository.Generic;

namespace AlmacenApi.Domain.Common.Interfaces.Repository.AdminRepo;
public interface IAdminRepository : IGenericRepository<AdminEntity>
{
    public Task<AdminEntity?> GetByEmail(string username , CancellationToken cancellationToken);
}