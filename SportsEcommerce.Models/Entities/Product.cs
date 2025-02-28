using Core.Entities;

namespace SportsEcommerce.Models.Entities;

public class Product : Entity<Guid>
{
  public Product()
  {
    
  }

  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public string ImageUrl { get; set; } = default!;
  public decimal Price { get; set; }
  public int Stock { get; set; }
  public bool IsActive { get; set; }
  public Category Category { get; set; }
  public int CategoryId { get; set; }
}
