using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

  public async Task<List<TEntity>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
    Expression<Func<TEntity, bool>>? predicate = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    CancellationToken cancellationToken = default)
  {
    IQueryable<TEntity> query = _context.Set<TEntity>();

    if (!enableTracking)
    {
      query = query.AsNoTracking();
    }

    if (withDeleted)
    {
      query = query.IgnoreQueryFilters();
    }

    if (include != null)
    {
      query = include(query);
    }

    if (predicate != null)
    {
      query = query.Where(predicate);
    }

    if (orderBy != null)
    {
      query = orderBy(query);
    }

    return await query.ToListAsync(cancellationToken);
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

  public void Delete(TEntity entity)
  {
    _context.Set<TEntity>().Remove(entity);
  }

  public void Update(TEntity entity)
  {
    entity.UpdatedDate = DateTime.UtcNow;
    _context.Set<TEntity>().Update(entity);
  }
}
