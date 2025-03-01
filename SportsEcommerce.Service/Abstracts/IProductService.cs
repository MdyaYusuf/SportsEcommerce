using Core.Responses;
using Microsoft.EntityFrameworkCore.Query;
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
    Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
    Expression<Func<Product, bool>>? predicate = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
    CancellationToken cancellationToken = default);
  Task<ReturnModel<ProductResponseDto?>> GetByIdAsync(Guid id);
  Task<ReturnModel<ProductResponseDto>> AddAsync(CreateProductRequest request);
  Task<ReturnModel<ProductResponseDto>> UpdateAsync(UpdateProductRequest request);
  Task<ReturnModel<ProductResponseDto>> RemoveAsync(Guid id);
}
