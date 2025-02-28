using Core.Entities;
using System.Linq.Expressions;

namespace Core.Repositories;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
{
  Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = false, CancellationToken cancellationToken = default);
  Task<TEntity?> GetByIdAsync(TId id);
  Task<TEntity> AddAsync(TEntity entity);
  Task<TEntity?> RemoveAsync(TEntity entity);
  Task<TEntity?> UpdateAsync(TEntity entity);
}
