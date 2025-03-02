using Core.Responses;
using Microsoft.EntityFrameworkCore.Query;
using SportsEcommerce.Models.Dtos.Categories.Requests;
using SportsEcommerce.Models.Dtos.Categories.Responses;
using SportsEcommerce.Models.Entities;
using System.Linq.Expressions;

namespace SportsEcommerce.Service.Abstracts;

public interface ICategoryService
{
  Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Func<IQueryable<Category>, IIncludableQueryable<Category, object>>? include = null,
    Expression<Func<Category, bool>>? predicate = null,
    Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
    CancellationToken cancellationToken = default);
  Task<ReturnModel<CategoryResponseDto>> GetByIdAsync(int id);
  Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest request);
  Task<ReturnModel<NoData>> RemoveAsync(int id);
  Task<ReturnModel<NoData>> UpdateAsync(UpdateCategoryRequest request);
}
