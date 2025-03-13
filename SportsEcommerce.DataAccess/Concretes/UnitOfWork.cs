using Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using SportsEcommerce.DataAccess.Contexts;

namespace SportsEcommerce.DataAccess.Concretes;

public class UnitOfWork : IUnitOfWork
{
  private readonly BaseDbContext _context;
  public UnitOfWork(BaseDbContext context)
  {
    _context = context;
  }

  public async Task<IDbContextTransaction> BeginTransactionAsync()
  {
    return await _context.Database.BeginTransactionAsync();
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }
}
