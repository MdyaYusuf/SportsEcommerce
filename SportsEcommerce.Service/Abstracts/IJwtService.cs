using SportsEcommerce.Models.Dtos.Tokens.Responses;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Abstracts;

public interface IJwtService
{
  Task<TokenResponseDto> CrreateJwtTokenAsync(User user);
}
