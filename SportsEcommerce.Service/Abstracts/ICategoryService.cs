using Core.Entities;
using Core.Responses;
using SportsEcommerce.Models.Dtos.Categories.Requests;
using SportsEcommerce.Models.Dtos.Categories.Responses;
using SportsEcommerce.Models.Entities;
using System.Linq.Expressions;

namespace SportsEcommerce.Service.Abstracts;

public interface ICategoryService
{
  Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync(Expression<Func<Category, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = false, CancellationToken cancellationToken = default);
  Task<ReturnModel<CategoryResponseDto?>> GetByIdAsync(int id);
  Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest request);
  Task<ReturnModel<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequest request);
  Task<ReturnModel<CategoryResponseDto>> RemoveAsync(int id);
}
