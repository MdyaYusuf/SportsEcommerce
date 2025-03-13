using SportsEcommerce.DataAccess.Abstracts;
using SportsEcommerce.DataAccess.Contexts;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Concretes;

public class EfOrderRepository : IOrderRepository
{
  protected BaseDbContext _context;
  public EfOrderRepository(BaseDbContext context)
  {
    _context = context;
  }

  public async Task<Order> CreateOrderAsync(Order order)
  {
    await _context.Orders.AddAsync(order);

    return order;
  }
}
