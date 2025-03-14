using Core.Exceptions;
using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.Service.Rules;

public class OrderBusinessRules(IOrderRepository _orderRepository)
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

  public async Task IsOrderExistAsync(int orderId)
  {
    var order = await _orderRepository.GetOrderByIdAsync(orderId);

    if (order == null)
    {
      throw new NotFoundException($"{orderId} numaralı sipariş bulunamadı.");
    }
  }
}
