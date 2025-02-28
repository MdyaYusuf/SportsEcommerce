using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositories;

public class EfBaseRepository<TContext, TEntity, TId> : IRepository<TEntity, TId>
  where TEntity : Entity<TId>, new()
  where TContext : DbContext
{
  protected TContext _context { get; }
  public EfBaseRepository(TContext context)
  {
    _context = context;
  }

  public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = false, CancellationToken cancellationToken = default)
  {
    return await _context.Set<TEntity>().ToListAsync();
  }

  public async Task<TEntity?> GetByIdAsync(TId id)
  {
    return await _context.Set<TEntity>().FindAsync(id);
  }

  public async Task<TEntity> AddAsync(TEntity entity)
  {
    entity.CreatedDate = DateTime.UtcNow;
    await _context.Set<TEntity>().AddAsync(entity);
    return entity;
  }

  public async Task<TEntity?> UpdateAsync(TEntity entity)
  {
    entity.UpdatedDate = DateTime.UtcNow;
    _context.Set<TEntity>().Update(entity);
    return entity;
  }

  public async Task<TEntity?> RemoveAsync(TEntity entity)
  {
    _context.Set<TEntity>().Remove(entity);
    return entity;
  }
}
