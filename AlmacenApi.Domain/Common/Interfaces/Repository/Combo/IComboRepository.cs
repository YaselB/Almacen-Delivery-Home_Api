using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Domain.Repository.Generic;

namespace AlmacenApi.Domain.Common.Interfaces.Repository.Combo;
public interface IComboRepository : IGenericRepository<ComboEntity>
{
    public Task<ComboEntity?> GetByName(string name , CancellationToken cancellationToken);
    public Task<IReadOnlyList<ComboEntity>> GetComboByAdmin(string id , CancellationToken cancellationToken);
    public Task<IReadOnlyList<ComboEntity>> GetComboyUser(string id , CancellationToken cancellationToken);
    public Task RemoveUserAndAdmin(string? UserId , string? AdminId , string comboId ,CancellationToken cancellationToken);
}