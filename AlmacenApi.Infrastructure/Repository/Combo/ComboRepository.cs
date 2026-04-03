using AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Infrastructure.DBContext;
using AlmacenApi.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenApi.Infrastructure.Repository.Combo;

public class ComboRepository : GenericRepository<ComboEntity>, IComboRepository
{
    private readonly AppDBContext dbContext;
    public ComboRepository(AppDBContext context) : base(context)
    {
        dbContext = context;
    }

    public async Task<ComboEntity?> GetByName(string name, CancellationToken cancellationToken)
    {
        var combo = await dbContext.Combo.FirstOrDefaultAsync(c => c.Name == name);
        if(combo == null)
        {
            return null;
        }
        return combo;
    }
    public override async Task<IReadOnlyList<ComboEntity>> FindALlAsync(CancellationToken cancellationToken)
    {
        var combos = await dbContext.Combo.Include(c => c.Admin).Include(c => c.User).ToListAsync();
        return combos;
    }
    public override async Task<ComboEntity?> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        var combo = await dbContext.Combo.Include(c => c.Admin).Include(c => c.User).FirstOrDefaultAsync(c => c.id == id);
        if(combo == null)
        {
            return null;
        }
        return combo;
    }

    public async Task<IReadOnlyList<ComboEntity>> GetComboByAdmin(string id, CancellationToken cancellationToken)
    {
        var combos = await dbContext.Combo.Where(p =>p.AdminId == id).ToListAsync();
        return combos;
    }

    public async Task<IReadOnlyList<ComboEntity>> GetComboyUser(string id, CancellationToken cancellationToken)
    {
        var combos = await dbContext.Combo.Where(c => c.UserId == id).ToListAsync();
        return combos;
    }

    public async Task RemoveUserAndAdmin(string? UserId, string? AdminId, string comboId, CancellationToken cancellationToken)
    {
        var combo = await dbContext.Combo.FirstOrDefaultAsync(c => c.id == comboId);
        if(combo == null)
        {
            return;
        }
        if(UserId != null)
        {
            combo.UserId = null;
        }
        if(AdminId != null)
        {
            combo.AdminId = null;
        }
        await UpdateAsync(combo, cancellationToken);
        return;
    }
}