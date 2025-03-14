using Core.Exceptions;
using SportsEcommerce.DataAccess.Abstracts;

namespace SportsEcommerce.Service.Rules;

public class ProductBusinessRules(IProductRepository _productRepository)
{
  public async Task IsProductExistAsync(Guid id)
  {
    var product = await _productRepository.GetByIdAsync(id);

    if (product == null)
    {
      throw new NotFoundException($"{id} numaralı ürün bulunamadı.");
    }
  }

  public async Task IsNameUnique(string name)
  {
    var product = await _productRepository.GetByNameAsync(name);

    if (product != null)
    {
      throw new BusinessException("Bu isim ile sistemimizde bir ürün zaten mevcut.");
    }
  }

  public async Task CheckSufficientStockAsync(Guid productId, int requiredQuantity)
  {
    var product = await _productRepository.GetByIdAsync(productId);

    if (product.Stock < requiredQuantity)
    {
      throw new BusinessException("Ürünün yeterli stoğu bulunmamaktadır.");
    }
  }
}
