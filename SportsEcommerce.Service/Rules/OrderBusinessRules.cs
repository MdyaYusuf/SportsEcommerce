using Core.Exceptions;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Rules;

public class OrderBusinessRules
{
  public void EnsureValidOrder(Order order)
  {
    if (order.Total <= 0)
    {
      throw new BusinessException("Sipariş toplamı sıfırdan büyük olmalıdır.");
    }

    if (string.IsNullOrWhiteSpace(order.OrderDetails))
    {
      throw new BusinessException("Sipariş detayı boş olamaz.");
    }
  }
}
