﻿using Core.Exceptions;
using SportsEcommerce.DataAccess.Abstracts;

namespace SportsEcommerce.Service.Rules;

public class CategoryBusinessRules(ICategoryRepository _categoryRepository)
{
  public async Task IsCategoryExistAsync(int id)
  {
    var category = await _categoryRepository.GetByIdAsync(id);

    if (category == null)
    {
      throw new NotFoundException($"{id} numaralı kategori bulunamadı.");
    }
  }

  public async Task IsNameUnique(string name)
  {
    var category = await _categoryRepository.GetByNameAsync(name);

    if (category != null)
    {
      throw new BusinessException("Bu isim ile sistemimizde bir kategori zaten mevcut.");
    }
  }
}
