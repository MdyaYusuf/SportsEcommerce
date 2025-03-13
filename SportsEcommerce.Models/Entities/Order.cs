using Core.Entities;

namespace SportsEcommerce.Models.Entities;

public class Order
{
  public Order()
  {
    
  }

  public int Id { get; set; }
  public DateTime OrderDate { get; set; }
  public decimal Total { get; set; }
  public string OrderDetails { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public User User { get; set; } = null!;
}
