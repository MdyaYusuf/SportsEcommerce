using Core.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SportsEcommerce.DataAccess.Concretes;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Concretes;
using SportsEcommerce.Service.Profiles;
using SportsEcommerce.Service.Rules;
using System.Reflection;

namespace SportsEcommerce.Service.Extensions;

public static class ServiceDependencies
{
  public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
  {
    services.AddAutoMapper(typeof(MappingProfiles));
    services.AddScoped<ProductBusinessRules>();
    services.AddScoped<CategoryBusinessRules>();
    services.AddScoped<UserBusinessRules>();
    services.AddScoped<RoleBusinessRules>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IJwtService, JwtService>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddFluentValidationAutoValidation();
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    return services;
  }
}
