using SportsEcommerce.Models.Dtos.Categories.Requests;
using SportsEcommerce.Models.Dtos.Categories.Responses;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Mappers;

public class CategoryMapper
{
  public Category ConvertToEntity(CreateCategoryRequest request)
  {
    return new Category()
    {
      Name = request.Name
    };
  }

  public Category ConvertToEntity(UpdateCategoryRequest request)
  {
    return new Category()
    {
      Name = request.Name
    };
  }

  public CategoryResponseDto ConvertToResponse(Category category)
  {
    return new CategoryResponseDto()
    {
      Name = category.Name
    };
  }

  public List<CategoryResponseDto> ConvertToResponseList(List<Category> categories)
  {
    return categories.Select(c => ConvertToResponse(c)).ToList();
  }
}
