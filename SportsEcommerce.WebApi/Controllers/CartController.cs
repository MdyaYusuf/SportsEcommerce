using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models.Dtos.Carts.Requests;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using System.Text.Json;

namespace SportsEcommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(ICartService _cartService, IProductService _productService, IHttpContextAccessor _httpContextAccessor, IMapper _mapper) : ControllerBase
{
  private const string SessionCartKey = "Cart";

  [HttpGet]
  public IActionResult GetCart()
  {
    var cart = GetCartFromSession();

    return Ok(cart);
  }

  [HttpPost("add")]
  public async Task<IActionResult> AddItemAsync([FromBody] AddCartItemRequest request)
  {
    var productResult = await _productService.GetByIdAsync(request.ProductId);
    var productEntity = _mapper.Map<Product>(productResult.Data);

    var cart = GetCartFromSession();
    _cartService.AddItem(cart, productEntity, request.Quantity);
    SaveCartToSession(cart);

    return Ok(cart);
  }

  [HttpPost("remove")]
  public IActionResult RemoveItem([FromBody] RemoveCartItemRequest request)
  {
    var cart = GetCartFromSession();
    _cartService.RemoveItem(cart, request.ProductId);
    SaveCartToSession(cart);

    return Ok(cart);
  }

  [HttpPost("clear")]
  public IActionResult ClearCart()
  {
    var cart = GetCartFromSession();
    _cartService.ClearCart(cart);
    SaveCartToSession(cart);

    return Ok(cart);
  }

  [HttpGet("total")]
  public IActionResult GetTotal()
  {
    var cart = GetCartFromSession();
    var total = _cartService.GetTotal(cart);

    return Ok(total);
  }

  private Cart GetCartFromSession()
  {
    var session = _httpContextAccessor.HttpContext.Session;
    var cartJson = session.GetString(SessionCartKey);

    if (string.IsNullOrEmpty(cartJson))
    {
      var newCart = new Cart();
      session.SetString(SessionCartKey, JsonSerializer.Serialize(newCart));

      return newCart;
    }

    return JsonSerializer.Deserialize<Cart>(cartJson);
  }

  private void SaveCartToSession(Cart cart)
  {
    var session = _httpContextAccessor.HttpContext.Session;
    session.SetString(SessionCartKey, JsonSerializer.Serialize(cart));
  }
}
