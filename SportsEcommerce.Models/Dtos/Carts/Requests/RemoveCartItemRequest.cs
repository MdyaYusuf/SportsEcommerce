namespace SportsEcommerce.Models.Dtos.Carts.Requests;

public sealed record RemoveCartItemRequest(Guid ProductId, int Quantity);
