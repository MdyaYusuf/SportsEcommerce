namespace SportsEcommerce.Models.Dtos.Products.Responses;

public sealed record ProductResponseDto
{
  public Guid Id { get; init; }
  public string Name { get; init; } = default!;
  public string Description { get; init; } = default!;
  public string ImageUrl { get; init; } = default!;
  public decimal Price { get; init; }
  public int Stock { get; init; }
  public string Category { get; init; } = default!;
}