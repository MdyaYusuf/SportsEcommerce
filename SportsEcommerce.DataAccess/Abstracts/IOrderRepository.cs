using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Abstracts;

public interface IOrderRepository
{
  Task<Order> CreateOrderAsync(Order order);
}
