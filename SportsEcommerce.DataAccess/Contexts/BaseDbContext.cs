using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsEcommerce.Models.Entities;
using System.Reflection;

namespace SportsEcommerce.DataAccess.Contexts;

public class BaseDbContext : IdentityDbContext<User, IdentityRole, string>
{
  public BaseDbContext(DbContextOptions opt) : base(opt)
  {
    
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    modelBuilder.Entity<Product>()
      .Navigation(p => p.Category)
      .AutoInclude();
  }

  public DbSet<Product> Products { get; set; }
  public DbSet<Category> Categories { get; set; }
}
