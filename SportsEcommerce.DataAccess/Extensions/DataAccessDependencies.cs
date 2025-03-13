using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.DataAccess.Concretes;
using SportsEcommerce.DataAccess.Contexts;

namespace SportsEcommerce.DataAccess.Extensions;

public static class DataAccessDependencies
{
  public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IProductRepository, EfProductRepository>();
    services.AddScoped<ICategoryRepository, EfCategoryRepository>();
    services.AddScoped<IOrderRepository, EfOrderRepository>();
    services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
    return services;
  }
}
