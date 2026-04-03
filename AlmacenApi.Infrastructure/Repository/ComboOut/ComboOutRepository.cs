using AlmacenApi.Domain.Common.Interfaces.Repository.ComboOut;
using AlmacenApi.Domain.Entities.CombOut;
using AlmacenApi.Domain.Repository.Generic;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;

namespace AlmacenApi.Infrastructure.Repository.ComboOut;

public class ComboOutRepository : GenericRepository<ComboOutEntity>, IComboOutRepository
{
    public ComboOutRepository(AppDBContext context) : base(context)
    {
    }
}