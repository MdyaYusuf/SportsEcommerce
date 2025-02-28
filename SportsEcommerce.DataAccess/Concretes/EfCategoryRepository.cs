using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.DataAccess.Contexts;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Concretes;

public class EfCategoryRepository : EfBaseRepository<BaseDbContext, Category, int>, ICategoryRepository
{
  public EfCategoryRepository(BaseDbContext context) : base(context)
  {

  }

  public async Task<Category?> GetByNameAsync(string name)
  {
    return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
  }
}
