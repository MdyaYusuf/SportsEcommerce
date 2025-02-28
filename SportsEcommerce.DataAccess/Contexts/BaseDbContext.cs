using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Contexts;

public class BaseDbContext : IdentityDbContext<User, IdentityRole, string>
{
  public BaseDbContext(DbContextOptions opt) : base(opt)
  {
    
  }

  public DbSet<Product> Products { get; set; }
  public DbSet<Category> Categories { get; set; }
}
