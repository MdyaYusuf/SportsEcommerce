using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models.Dtos.Orders.Requests;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.WebApi.Helpers;
using System.Security.Claims;

namespace SportsEcommerce.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService _orderService, CartSessionHelper _helper) : ControllerBase
{
  [HttpGet("getbyid/{id}")]
  public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
  {
    var result = await _orderService.GetOrderByIdAsync(id);
    return Ok(result);
  }

  [HttpPost("create")]
  public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

    var cart = _helper.GetCartFromSession();

    var result = await _orderService.CreateOrderAsync(request, cart, userId);

    return Ok(result);
  }
}
