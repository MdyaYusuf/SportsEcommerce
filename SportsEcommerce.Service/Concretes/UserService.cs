using Core.Utils;
using Microsoft.AspNetCore.Identity;
using SportsEcommerce.Models.Dtos.Users.Requests;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Rules;

namespace SportsEcommerce.Service.Concretes;

public sealed class UserService(UserManager<User> _userManager, UserBusinessRules _businessRules) : IUserService
{
  public async Task<User> GetByUsernameAsync(string username)
  {
    var user = await _businessRules.EnsureUserExistsByUsernameAsync(username);

    return user;
  }

  public async Task<User> GetByEmailAsync(string email)
  {
    var user = await _businessRules.EnsureUserExistsByEmailAsync(email);

    return user;
  }

  public async Task<User> RegisterAsync(RegisterRequest request)
  {
    await _businessRules.IsUsernameUniqueAsync(request.Username);

    User user = new User
    {
      FirstName = request.FirstName,
      LastName = request.LastName,
      Email = request.Email,
      City = request.City,
      UserName = request.Username,
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    IdentityResultHelper.Check(result);

    var addRole = await _userManager.AddToRoleAsync(user, "Customer");
    IdentityResultHelper.Check(addRole);

    return user;
  }

  public async Task<User> LoginAsync(LoginRequest request)
  {
    var user = await _businessRules.EnsureUserExistsByEmailAsync(request.Email);

    await _businessRules.CheckUserPasswordAsync(user, request.Password);

    return user;
  }

  public async Task<User> ChangePasswordAsync(string id, ChangePasswordRequest request)
  {
    var user = await _businessRules.EnsureUserExistsAsync(id);
    _businessRules.EnsurePasswordsMatch(request.NewPassword, request.ConfirmNewPassword);

    var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
    IdentityResultHelper.Check(result);

    return user;
  }

  public async Task<User> UpdateAsync(string id, UserUpdateRequest request)
  {
    var user = await _businessRules.EnsureUserExistsAsync(id);
    await _businessRules.IsUsernameUniqueAsync(request.Username);

    user.UserName = request.Username;
    user.FirstName = request.FirstName;
    user.LastName = request.LastName;
    user.City = request.City;

    var result = await _userManager.UpdateAsync(user);
    IdentityResultHelper.Check(result);

    return user;
  }

  public async Task<string> DeleteAsync(string id)
  {
    var user = await _businessRules.EnsureUserExistsAsync(id);

    var result = await _userManager.DeleteAsync(user);

    IdentityResultHelper.Check(result);

    return "Kullanıcı silindi.";
  }
}
