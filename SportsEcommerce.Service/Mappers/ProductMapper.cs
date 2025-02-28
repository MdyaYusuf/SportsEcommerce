using SportsEcommerce.Models.Dtos.Products.Requests;
using SportsEcommerce.Models.Dtos.Products.Responses;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Mappers;

public class ProductMapper
{
  public Product ConvertToEntity(CreateProductRequest request)
  {
    return new Product()
    {
      Name = request.Name,
      Description = request.Description,
      ImageUrl = request.ImageUrl,
      Price = request.Price,
      Stock = request.Stock,
      IsActive = request.IsActive
    };
  }

  public Product ConvertToEntity(UpdateProductRequest request)
  {
    return new Product()
    {
      Id = request.Id,
      Name = request.Name,
      Description = request.Description,
      ImageUrl = request.ImageUrl,
      Price = request.Price,
      Stock = request.Stock,
      IsActive = request.IsActive
    };
  }

  public ProductResponseDto ConvertToResponse(Product product)
  {
    return new ProductResponseDto()
    {
      Name = product.Name,
      Price = product.Price,
      Stock = product.Stock
    };
  }

  public List<ProductResponseDto> ConvertToResponseList(List<Product> products)
  {
    return products.Select(p => ConvertToResponse(p)).ToList();
  }
}
