using Core.Entities;

namespace SportsEcommerce.Models.Entities;

public class Category : Entity<int>
{
  public Category()
  {
    
  }

  public string Name { get; set; }
  public List<Product>? Products { get; set; }
}
