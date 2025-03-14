using SportsEcommerce.Models.Entities;
using System.Text.Json;

namespace SportsEcommerce.WebApi.Helpers
{
  public class CartSessionHelper(IHttpContextAccessor _httpContextAccessor)
  {
    private const string SessionCartKey = "Cart";

    public Cart GetCartFromSession()
    {
      var session = _httpContextAccessor.HttpContext.Session;

      var cartJson = session.GetString(SessionCartKey);

      if (string.IsNullOrEmpty(cartJson))
      {
        var newCart = new Cart();

        session.SetString(SessionCartKey, JsonSerializer.Serialize(newCart));

        return newCart;
      }

      return JsonSerializer.Deserialize<Cart>(cartJson);
    }

    public void SaveCartToSession(Cart cart)
    {
      var session = _httpContextAccessor.HttpContext.Session;

      session.SetString(SessionCartKey, JsonSerializer.Serialize(cart));
    }
  }
}

