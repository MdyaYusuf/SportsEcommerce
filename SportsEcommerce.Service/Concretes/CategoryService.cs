using AutoMapper;
using Core.Abstractions;
using Core.Responses;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.Models.Dtos.Categories.Requests;
using SportsEcommerce.Models.Dtos.Categories.Responses;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Rules;
using System.Linq.Expressions;

namespace SportsEcommerce.Service.Concretes;

public class CategoryService(ICategoryRepository _categoryRepository, CategoryBusinessRules _businessRules, IMapper _mapper, IUnitOfWork _unitOfWork) : ICategoryService
{
  public async Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest request)
  {
    await _businessRules.IsNameUnique(request.Name);

    Category createdCategory = _mapper.Map<Category>(request);
    await _categoryRepository.AddAsync(createdCategory);
    await _unitOfWork.SaveChangesAsync();
    CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(createdCategory);

    return new ReturnModel<CategoryResponseDto>()
    {
      Success = true,
      Message = "Kategori eklendi.",
      Data = response,
      StatusCode = 201
    };
  }

  public async Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync(
    bool enableTracking = false,
    bool withDeleted = false,
    Expression<Func<Category, bool>>? filter = null,
    Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
    CancellationToken cancellationToken = default)
  {
    List<Category> categories = await _categoryRepository.GetAllAsync(
      enableTracking,
      withDeleted,
      filter,
      orderBy,
      cancellationToken);

    List<CategoryResponseDto> responseList = _mapper.Map<List<CategoryResponseDto>>(categories);

    return new ReturnModel<List<CategoryResponseDto>>()
    {
      Success = true,
      Message = "Kategori listesi başarılı bir şekilde getirildi.",
      Data = responseList,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<CategoryResponseDto>> GetByIdAsync(int id)
  {
    await _businessRules.IsCategoryExistAsync(id);

    Category category = await _categoryRepository.GetByIdAsync(id);
    CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(category);

    return new ReturnModel<CategoryResponseDto>()
    {
      Success = true,
      Message = $"{id} numaralı kategori başarılı bir şekilde getirildi.",
      Data = response,
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> RemoveAsync(int id)
  {
    await _businessRules.IsCategoryExistAsync(id);

    Category category = await _categoryRepository.GetByIdAsync(id);
    _categoryRepository.Delete(category);
    await _unitOfWork.SaveChangesAsync();

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Kategori başarılı bir şekilde silindi.",
      StatusCode = 200
    };
  }

  public async Task<ReturnModel<NoData>> UpdateAsync(UpdateCategoryRequest request)
  {
    await _businessRules.IsCategoryExistAsync(request.Id);

    Category existingCategory = await _categoryRepository.GetByIdAsync(request.Id);

    existingCategory.Id = request.Id;
    existingCategory.Name = request.Name;

    _categoryRepository.Update(existingCategory);
    await _unitOfWork.SaveChangesAsync();

    return new ReturnModel<NoData>()
    {
      Success = true,
      Message = "Kategori başarılı bir şekilde güncellendi.",
      StatusCode = 200
    };
  }
}
