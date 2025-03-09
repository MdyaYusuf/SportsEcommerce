using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models.Dtos.Users.Requests;
using SportsEcommerce.Service.Abstracts;

namespace SportsEcommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IAuthenticationService _authenticationService) : ControllerBase
{
  [HttpPost("login")]
  public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
  {
    var result = await _authenticationService.LoginAsync(request);
    return Ok(result);
  }

  [HttpPost("register")]
  public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
  {
    var result = await _authenticationService.RegisterAsync(request);
    return Ok(result);
  }
}
