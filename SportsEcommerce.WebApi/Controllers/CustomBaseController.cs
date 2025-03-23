using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SportsEcommerce.WebApi.Controllers;

public class CustomBaseController : ControllerBase
{
  [NonAction]
  public string GetUserId()
  {
    return HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new AuthorizationException("Giriş yapmalısınız.");
  }
}
