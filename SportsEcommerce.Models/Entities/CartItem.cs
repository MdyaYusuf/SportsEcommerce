using Core.Entities;

namespace SportsEcommerce.Models.Entities;

public class CartItem : Entity<int>
{
  public CartItem()
  {

  }

  public int Quantity { get; set; }
  public int CartId { get; set; }
  public Cart Cart { get; set; } = null!;
  public Guid ProductId { get; set; }
  public Product Product { get; set; } = null!;
}
