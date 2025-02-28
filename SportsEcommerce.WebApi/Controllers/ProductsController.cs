using Microsoft.AspNetCore.Mvc;
using SportsEcommerce.Models.Dtos.Products.Requests;
using SportsEcommerce.Service.Abstracts;

namespace SportsEcommerce.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService _productService) : ControllerBase
{
  [HttpGet("getall")]
  public async Task<IActionResult> GetAllAsync()
  {
    var result = await _productService.GetAllAsync();
    return Ok(result);
  }

  [HttpPost("add")]
  public async Task<IActionResult> AddAsync([FromBody] CreateProductRequest request)
  {
    var result = await _productService.AddAsync(request);
    return Ok(result);
  }

  [HttpGet("getbyid/{id}")]
  public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
  {
    var result = await _productService.GetByIdAsync(id);
    return Ok(result);
  }

  [HttpDelete("delete")]
  public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
  {
    var result = await _productService.RemoveAsync(id);
    return Ok(result);
  }

  [HttpPut("update")]
  public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest request)
  {
    var result = await _productService.UpdateAsync(request);
    return Ok(result);
  }
}
