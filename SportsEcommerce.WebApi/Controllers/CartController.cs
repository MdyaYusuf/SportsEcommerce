using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models.Dtos.Carts.Requests;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.WebApi.Helpers;

namespace SportsEcommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(ICartService _cartService, IProductService _productService, IMapper _mapper, CartSessionHelper _helper) : ControllerBase
{
  [HttpGet]
  public IActionResult GetCart()
  {
    var cart = _helper.GetCartFromSession();

    return Ok(cart);
  }

  [HttpPost("add")]
  public async Task<IActionResult> AddItemAsync([FromBody] AddCartItemRequest request)
  {
    var productResult = await _productService.GetByIdAsync(request.ProductId);
    var productEntity = _mapper.Map<Product>(productResult.Data);

    var cart = _helper.GetCartFromSession();
    _cartService.AddItem(cart, productEntity, request.Quantity);
    _helper.SaveCartToSession(cart);

    return Ok(cart);
  }

  [HttpPost("remove")]
  public IActionResult RemoveItem([FromBody] RemoveCartItemRequest request)
  {
    var cart = _helper.GetCartFromSession();
    _cartService.RemoveItem(cart, request.ProductId);
    _helper.SaveCartToSession(cart);

    return Ok(cart);
  }

  [HttpPost("clear")]
  public IActionResult ClearCart()
  {
    var cart = _helper.GetCartFromSession();
    _cartService.ClearCart(cart);
    _helper.SaveCartToSession(cart);

    return Ok(cart);
  }

  [HttpGet("total")]
  public IActionResult GetTotal()
  {
    var cart = _helper.GetCartFromSession();
    var total = _cartService.GetTotal(cart);

    return Ok(total);
  }
}
