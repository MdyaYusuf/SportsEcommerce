using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Abstracts;

public interface ICartService
{
  void AddItem(Cart cart, Product product, int quantity);
  void RemoveItem(Cart cart, Guid productId);
  void ClearCart(Cart cart);
  decimal GetTotal(Cart cart);
}
