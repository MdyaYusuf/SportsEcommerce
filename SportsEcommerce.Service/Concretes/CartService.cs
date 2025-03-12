using Core.Exceptions;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Abstracts;
using SportsEcommerce.Service.Rules;

namespace SportsEcommerce.Service.Concretes;

public class CartService(CartBusinessRules _businessRules) : ICartService
{
  public void AddItem(Cart cart, Product product, int quantity)
  {
    if (quantity <= 0)
    {
      throw new BusinessException("Adet sayısı sıfırdan büyük olmalıdır.");
    }

    var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == product.Id);
    int currentQuantity = existingItem?.Quantity ?? 0;

    _businessRules.EnsureStockAvailable(product, currentQuantity, quantity);

    if (existingItem != null)
    {
      existingItem.Quantity += quantity;
    }
    else
    {
      cart.CartItems.Add(new CartItem()
      {
        ProductId = product.Id,
        ProductName = product.Name,
        UnitPrice = product.Price,
        Quantity = quantity
      });
    }
  }

  public void RemoveItem(Cart cart, Guid productId)
  {
    var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

    if (item != null)
    {
      cart.CartItems.Remove(item);
    }
  }

  public void ClearCart(Cart cart)
  {
    cart.CartItems.Clear();
  }

  public decimal GetTotal(Cart cart)
  {
    return cart.CartItems.Sum(ci => ci.UnitPrice * ci.Quantity);
  }
}