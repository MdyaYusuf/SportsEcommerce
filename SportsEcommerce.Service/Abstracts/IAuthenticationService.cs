using Core.Responses;
using SportsEcommerce.Models.Dtos.Tokens.Responses;
using SportsEcommerce.Models.Dtos.Users.Requests;

namespace SportsEcommerce.Service.Abstracts;

public interface IAuthenticationService
{
  Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequest request);
  Task<ReturnModel<TokenResponseDto>> RegisterAsync(RegisterRequest request);
}
