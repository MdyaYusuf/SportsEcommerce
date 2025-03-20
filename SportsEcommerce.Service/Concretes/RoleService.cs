using Core.Utils;
using Microsoft.AspNetCore.Identity;
using SportsEcommerce.Models.Dtos.Users.Requests;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Rules;

namespace SportsEcommerce.Service.Concretes;

public class RoleService : IRoleService
{
  private readonly UserManager<User> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;
  private readonly RoleBusinessRules _roleBusinessRules;
  public RoleService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, RoleBusinessRules roleBusinessRules)
  {
    _userManager = userManager;
    _roleManager = roleManager;
    _roleBusinessRules = roleBusinessRules;
  }

  public async Task<string> AddRoleAsync(string name)
  {
    await _roleBusinessRules.IsRoleUniqueAsync(name);

    var role = new IdentityRole()
    {
      Name = name
    };

    var result = await _roleManager.CreateAsync(role);
    IdentityResultHelper.Check(result);

    return $"{name} isimli rol eklendi.";
  }

  public async Task<string> AddRoleToUser(AddRoleToUserRequest request)
  {
    var role = await _roleManager.FindByNameAsync(request.RoleName);
    _roleBusinessRules.EnsureRoleExist(role);

    var user = await _userManager.FindByIdAsync(request.UserId);
    _roleBusinessRules.EnsureUserExist(user);

    var addRoleToUser = await _userManager.AddToRoleAsync(user, request.RoleName);
    IdentityResultHelper.Check(addRoleToUser);

    return $"Kullanıcıya {request.RoleName} isimli rol eklendi.";
  }

  public async Task<List<string>> GetAllRolesByUserId(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);
    _roleBusinessRules.EnsureUserExist(user);

    var roles = await _userManager.GetRolesAsync(user);

    return roles.ToList();
  }
}
