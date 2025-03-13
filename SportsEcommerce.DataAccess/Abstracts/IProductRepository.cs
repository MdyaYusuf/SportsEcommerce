using Core.Repositories;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Abstracts;

public interface IProductRepository : IRepository<Product, Guid>
{
  Task<Product?> GetByNameAsync(string name);
  Task ReduceStockAsync(Guid productId, int quantity);
}
