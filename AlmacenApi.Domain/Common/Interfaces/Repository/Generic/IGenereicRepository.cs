
using AlmacenApi.Domain.Common;

namespace AlmacenApi.Domain.Repository.Generic;
public interface IGenericRepository<T>
where T : GenericEntity<T>
{
    public Task<T?> FindByIdAsync(string id , CancellationToken cancellationToken);
    public Task<IReadOnlyList<T>> FindALlAsync(CancellationToken cancellationToken);
    public Task<T> AddAsync(T entity , CancellationToken cancellationToken);
    public Task UpdateAsync(T entity ,CancellationToken cancellationToken);
    public Task RemoveAsync(T entity , CancellationToken cancellationToken);
}