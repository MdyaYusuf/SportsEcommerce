namespace SportsEcommerce.Models.Dtos.Orders.Responses;

public sealed record OrderResponseDto(int OrderId, string UserId, DateTime OrderDate, decimal Total, string OrderDetails);
