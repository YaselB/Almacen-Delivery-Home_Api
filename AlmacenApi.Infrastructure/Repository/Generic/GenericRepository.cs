using Microsoft.EntityFrameworkCore;
using AlmacenApi.Domain.Common;
using AlmacenApi.Domain.Repository.Generic;
using AlmacenApi.Infrastructure.DBContext;

namespace AlmacenApi.Infrastructure.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : GenericEntity<T>
    {
        protected readonly AppDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task<IReadOnlyList<T>> FindALlAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            var result = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            return result;
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var existing = await FindByIdAsync(entity.id, cancellationToken);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                existing.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task RemoveAsync(T entity, CancellationToken cancellationToken)
        {
            var existing = await FindByIdAsync(entity.id, cancellationToken);
            if (existing != null)
            {
                existing.UpdatedAt = DateTime.UtcNow;
                _dbSet.Remove(existing);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}