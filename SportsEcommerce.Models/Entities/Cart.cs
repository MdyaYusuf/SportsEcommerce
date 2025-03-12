namespace SportsEcommerce.Models.Entities;

public class Cart
{
  public Cart()
  {
    
  }

  public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}

