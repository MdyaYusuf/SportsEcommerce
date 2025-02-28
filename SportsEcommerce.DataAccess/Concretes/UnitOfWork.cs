using Core.Abstractions;
using SportsEcommerce.DataAccess.Contexts;

namespace SportsEcommerce.DataAccess.Concretes;

public class UnitOfWork : IUnitOfWork
{
  private readonly BaseDbContext _context;
  public UnitOfWork(BaseDbContext context)
  {
    _context = context;
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }
}
