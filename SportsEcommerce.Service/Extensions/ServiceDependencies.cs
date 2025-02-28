using Microsoft.Extensions.DependencyInjection;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Concretes;
using SportsEcommerce.Service.Mappers;
using SportsEcommerce.Service.Rules;

namespace SportsEcommerce.Service.Extensions;

public static class ServiceDependencies
{
  public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
  {
    services.AddScoped<ProductMapper>();
    services.AddScoped<CategoryMapper>();
    services.AddScoped<ProductBusinessRules>();
    services.AddScoped<CategoryBusinessRules>();
    services.AddScoped<UserBusinessRules>();
    services.AddScoped<RoleBusinessRules>();
    services.AddScoped<IJwtService, JwtService>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ICategoryService, CategoryService>();

    return services;
  }
}
