using Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Rules;

public class UserBusinessRules(UserManager<User> _userManager)
{
  public async Task<User> EnsureUserExistsAsync(string id)
  {
    var user = await _userManager.FindByIdAsync(id);

    if (user == null)
    {
      throw new NotFoundException("Kullanıcı bulunamadı.");
    }

    return user;
  }

  public async Task<User> EnsureUserExistsByEmailAsync(string email)
  {
    var user = await _userManager.FindByEmailAsync(email);

    if (user == null)
    {
      throw new AuthorizationException("Giriş bilgileriniz yanlış.");
    }

    return user;
  }

  public async Task<User> EnsureUserExistsByUsernameAsync(string username)
  {
    var user = await _userManager.FindByNameAsync(username);

    if (user == null)
    {
      throw new NotFoundException("Kullanıcı bulunamadı.");
    }

    return user;
  }

  public async Task CheckUserPasswordAsync(User user, string password)
  {
    bool checkPassword = await _userManager.CheckPasswordAsync(user, password);

    if (!checkPassword)
    {
      throw new AuthorizationException("Giriş bilgileriniz yanlış.");
    }
  }

  public void EnsurePasswordsMatch(string newPassword, string confirmNewPassword)
  {
    if (!newPassword.Equals(confirmNewPassword))
    {
      throw new BusinessException("Parolalar uyuşmuyor.");
    }
  }

  public async Task IsUsernameUniqueAsync(string username)
  {
    var existingUser = await _userManager.FindByNameAsync(username);

    if (existingUser != null)
    {
      throw new BusinessException($"{username} kullanıcı adı daha önceden alınmış.");
    }
  }
}
