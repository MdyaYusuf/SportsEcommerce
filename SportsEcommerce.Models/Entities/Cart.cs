using Core.Entities;

namespace SportsEcommerce.Models.Entities;

public class Cart : Entity<int>
{
  public Cart()
  {
    
  }

  public string CustomerId { get; set; } = null!;
  public User Customer { get; set; } = null!;
  public ICollection<CartItem>? CartItems { get; set; }
}
