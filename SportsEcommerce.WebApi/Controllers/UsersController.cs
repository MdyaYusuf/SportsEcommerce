using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Service.Abstracts;

namespace SportsEcommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService _userService) : ControllerBase
{
  [HttpGet("email")]
  [Authorize(Roles = "admin")]
  public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
  {
    var result = await _userService.GetByEmailAsync(email);
    return Ok(result);
  }
}
