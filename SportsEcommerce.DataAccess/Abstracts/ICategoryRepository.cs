using Core.Repositories;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Abstracts;

public interface ICategoryRepository : IRepository<Category, int>
{
  Task<Category?> GetByNameAsync(string name);
}
