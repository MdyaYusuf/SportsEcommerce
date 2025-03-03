using Core.Entities;
using System.Linq.Expressions;

namespace Core.Repositories;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
{
  Task<List<TEntity>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Expression<Func<TEntity, bool>>? filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    CancellationToken cancellationToken = default);
  Task<TEntity?> GetByIdAsync(TId id);
  Task<TEntity> AddAsync(TEntity entity);
  void Delete(TEntity entity);
  void Update(TEntity entity);
}
