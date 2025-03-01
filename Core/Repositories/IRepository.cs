using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repositories;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
{
  Task<List<TEntity>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    CancellationToken cancellationToken = default);
  Task<TEntity?> GetByIdAsync(TId id);
  Task<TEntity> AddAsync(TEntity entity);
  Task<TEntity?> RemoveAsync(TEntity entity);
  Task<TEntity?> UpdateAsync(TEntity entity);
}
