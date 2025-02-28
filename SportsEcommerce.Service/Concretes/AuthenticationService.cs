using Core.Responses;
using SportsEcommerce.Models.Dtos.Tokens.Responses;
using SportsEcommerce.Models.Dtos.Users.Requests;
using SportsEcommerce.Service.Abstracts;

namespace SportsEcommerce.Service.Concretes;

public class AuthenticationService(IUserService _userService, IJwtService _jwtService) : IAuthenticationService
{
  public async Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequest request)
  {
    var user = await _userService.LoginAsync(request);
    var tokenResponse = await _jwtService.CrreateJwtTokenAsync(user);

    return new ReturnModel<TokenResponseDto>()
    {
      Data = tokenResponse,
      Message = "Giriş başarılı.",
      StatusCode = 200,
      Success = true
    };
  }

  public async Task<ReturnModel<TokenResponseDto>> RegisterAsync(RegisterRequest request)
  {
    var user = await _userService.RegisterAsync(request);
    var tokenResponse = await _jwtService.CrreateJwtTokenAsync(user);

    return new ReturnModel<TokenResponseDto>()
    {
      Data = tokenResponse,
      Message = "Giriş başarılı.",
      StatusCode = 200,
      Success = true
    };
  }
}
