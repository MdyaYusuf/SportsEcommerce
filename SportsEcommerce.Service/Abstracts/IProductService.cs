using Core.Responses;
using SportsEcommerce.Models.Dtos.Products.Requests;
using SportsEcommerce.Models.Dtos.Products.Responses;
using SportsEcommerce.Models.Entities;
using System.Linq.Expressions;

namespace SportsEcommerce.Service.Abstracts;

public interface IProductService
{
  Task<ReturnModel<List<ProductResponseDto>>> GetAllAsync(Expression<Func<Product, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = false, CancellationToken cancellationToken = default);
  Task<ReturnModel<ProductResponseDto?>> GetByIdAsync(Guid id);
  Task<ReturnModel<ProductResponseDto>> AddAsync(CreateProductRequest request);
  Task<ReturnModel<ProductResponseDto>> UpdateAsync(UpdateProductRequest request);
  Task<ReturnModel<ProductResponseDto>> RemoveAsync(Guid id);
}
