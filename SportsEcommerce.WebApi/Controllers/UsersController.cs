using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models.Dtos.Users.Requests;
using SportsEcommerce.Service.Abstracts;

namespace SportsEcommerce.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService _userService) : CustomBaseController
{
  [HttpGet("getByEmail")]
  public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
  {
    var result = await _userService.GetByEmailAsync(email);

    return Ok(result);
  }

  [HttpGet("getByUsername")]
  public async Task<IActionResult> GetByUsernameAsync([FromQuery] string username)
  {
    var result = await _userService.GetByUsernameAsync(username);

    return Ok(result);
  }

  [HttpPut("update/{id}")]
  public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UserUpdateRequest request)
  {
    var result = await _userService.UpdateAsync(id, request);

    return Ok(result);
  }

  [HttpDelete("delete/{id}")]
  public async Task<IActionResult> DeleteAsync([FromRoute] string id)
  {
    var result = await _userService.DeleteAsync(id);

    return Ok(result);
  }

  [HttpPut("changePassword/{id}")]
  public async Task<IActionResult> ChangePasswordAsync([FromRoute] string id, [FromBody] ChangePasswordRequest request)
  {
    var user = await _userService.ChangePasswordAsync(id, request);

    return Ok(user);
  }
}
