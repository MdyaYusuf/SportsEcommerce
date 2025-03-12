using Core.Exceptions;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Rules;

public class CartBusinessRules
{
  public void EnsureStockAvailable(Product product, int currentQuantity, int additionalQuantity)
  {
    if (product.Stock < currentQuantity + additionalQuantity)
    {
      throw new BusinessException("Ürün stoğu yeterli değil.");
    }
  }
}
