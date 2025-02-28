namespace SportsEcommerce.Models.Dtos.Products.Responses;

public sealed record ProductResponseDto
{
  public string Name { get; init; } = default!;
  public decimal Price { get; init; }
  public int Stock { get; init; }
}