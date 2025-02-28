using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.DataAccess.Contexts;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Concretes;

public class EfProductRepository : EfBaseRepository<BaseDbContext, Product, Guid>, IProductRepository
{
  public EfProductRepository(BaseDbContext context) : base(context)
  {

  }

  public async Task<Product?> GetByNameAsync(string name)
  {
    return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
  }
}
