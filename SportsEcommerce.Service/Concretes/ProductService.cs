using AutoMapper;
using Core.Abstractions;
using Core.Responses;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.Models.Dtos.Products.Requests;
using SportsEcommerce.Models.Dtos.Products.Responses;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Rules;
using System.Linq.Expressions;

namespace SportsEcommerce.Service.Concretes;

public class ProductService(IProductRepository _productRepository, ProductBusinessRules _businessRules, IMapper _mapper, IUnitOfWork _unitOfWork) : IProductService
{
  public async Task<ReturnModel<CreatedProductResponseDto>> AddAsync(CreateProductRequest request)
  {
    await _businessRules.IsNameUnique(request.Name);

    Product createdProduct = _mapper.Map<Product>(request);
    await _productRepository.AddAsync(createdProduct);
    await _unitOfWork.SaveChangesAsync();
    CreatedProductResponseDto response = _mapper.Map<CreatedProductResponseDto>(createdProduct);

    return new ReturnModel<CreatedProductResponseDto>()
    {
      Success = true,
      Message = "Ürün başarılı bir şekilde eklendi.",
      Data = response,
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<ProductResponseDto>>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Expression<Func<Product, bool>>? filter = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
    CancellationToken cancellationToken = default)
  {
    List<Product> products = await _productRepository.GetAllAsync(
      enableTracking,
      withDeleted,
      filter,
      orderBy,
      cancellationToken);

    List<ProductResponseDto> responseList = _mapper.Map<List<ProductResponseDto>>(products);

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
    ProductResponseDto response = _mapper.Map<ProductResponseDto>(product);

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
    existingProduct.CategoryId = request.CategoryId;

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
