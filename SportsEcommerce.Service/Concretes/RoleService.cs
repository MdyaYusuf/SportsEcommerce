using Core.Utils;
using Microsoft.AspNetCore.Identity;
using SportsEcommerce.Models.Dtos.Users.Requests;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Rules;

namespace SportsEcommerce.Service.Concretes;

public class RoleService(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, RoleBusinessRules _businessRules) : IRoleService
{
  public async Task<string> AddRoleAsync(string name)
  {
    await _businessRules.IsRoleUniqueAsync(name);

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
    _businessRules.EnsureRoleExist(role);

    var user = await _userManager.FindByIdAsync(request.UserId);
    _businessRules.EnsureUserExist(user);

    var addRoleToUser = await _userManager.AddToRoleAsync(user, request.RoleName);
    IdentityResultHelper.Check(addRoleToUser);

    return $"Kullanıcıya {request.RoleName} isimli rol eklendi.";
  }

  public async Task<List<string>> GetAllRolesByUserId(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);
    _businessRules.EnsureUserExist(user);

    var roles = await _userManager.GetRolesAsync(user);

    return roles.ToList();
  }
}
