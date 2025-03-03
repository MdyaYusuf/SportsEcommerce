using Core.Responses;
using SportsEcommerce.Models.Dtos.Products.Requests;
using SportsEcommerce.Models.Dtos.Products.Responses;
using SportsEcommerce.Models.Entities;
using System.Linq.Expressions;

namespace SportsEcommerce.Service.Abstracts;

public interface IProductService
{
  Task<ReturnModel<List<ProductResponseDto>>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Expression<Func<Product, bool>>? filter = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
    CancellationToken cancellationToken = default);
  Task<ReturnModel<ProductResponseDto>> GetByIdAsync(Guid id);
  Task<ReturnModel<CreatedProductResponseDto>> AddAsync(CreateProductRequest request);
  Task<ReturnModel<NoData>> RemoveAsync(Guid id);
  Task<ReturnModel<NoData>> UpdateAsync(UpdateProductRequest request);
}
