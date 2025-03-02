using Core.Abstractions;
using Core.Responses;
using Microsoft.EntityFrameworkCore.Query;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.Models.Dtos.Products.Requests;
using SportsEcommerce.Models.Dtos.Products.Responses;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Mappers;
using SportsEcommerce.Service.Rules;
using System.Linq.Expressions;

namespace SportsEcommerce.Service.Concretes;

public class ProductService(IProductRepository _productRepository, ProductBusinessRules _businessRules, ProductMapper _mapper, IUnitOfWork _unitOfWork) : IProductService
{
  public async Task<ReturnModel<ProductResponseDto>> AddAsync(CreateProductRequest request)
  {
    await _businessRules.IsNameUnique(request.Name);

    Product createdProduct = _mapper.ConvertToEntity(request);
    await _productRepository.AddAsync(createdProduct);
    await _unitOfWork.SaveChangesAsync();
    ProductResponseDto response = _mapper.ConvertToResponse(createdProduct);

    return new ReturnModel<ProductResponseDto>()
    {
      Success = true,
      Message = "Ürün eklendi.",
      Data = response,
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<ProductResponseDto>>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
    Expression<Func<Product, bool>>? predicate = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
    CancellationToken cancellationToken = default)
  {
    List<Product> products = await _productRepository.GetAllAsync(
      enableTracking,
      withDeleted,
      include,
      predicate,
      orderBy,
      cancellationToken);

    List<ProductResponseDto> responseList = _mapper.ConvertToResponseList(products);

    return new ReturnModel<List<ProductResponseDto>>()
    {
      Success = true,
      Message = "Ürün listesi başarılı bir şekilde getirildi.",
      Data = responseList,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<ProductResponseDto>> GetByIdAsync(Guid id)
  {
    await _businessRules.IsProductExistAsync(id);

    Product product = await _productRepository.GetByIdAsync(id);
    ProductResponseDto response = _mapper.ConvertToResponse(product);

    return new ReturnModel<ProductResponseDto>()
    {
      Success = true,
      Message = $"{id} numaralı ürün başarılı bir şekilde getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RemoveAsync(Guid id)
  {
    await _businessRules.IsProductExistAsync(id);

    Product product = await _productRepository.GetByIdAsync(id);
    _productRepository.Delete(product);
    await _unitOfWork.SaveChangesAsync();

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Ürün başarılı bir şekilde silindi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> UpdateAsync(UpdateProductRequest request)
  {
    await _businessRules.IsProductExistAsync(request.Id);

    Product existingProduct = await _productRepository.GetByIdAsync(request.Id);

    existingProduct.Id = request.Id;
    existingProduct.Name = request.Name;
    existingProduct.Description = request.Description;
    existingProduct.ImageUrl = request.ImageUrl;
    existingProduct.Price = request.Price;
    existingProduct.Stock = request.Stock;
    existingProduct.IsActive = request.IsActive;

    _productRepository.Update(existingProduct);
    await _unitOfWork.SaveChangesAsync();

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Ürün başarılı bir şekilde güncellendi.",
      StatusCode = 200
    };
  }
}
