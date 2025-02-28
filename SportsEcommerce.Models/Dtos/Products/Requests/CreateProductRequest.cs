namespace SportsEcommerce.Models.Dtos.Products.Requests;

public sealed record CreateProductRequest(string Name, string Description, string ImageUrl, decimal Price, int Stock, bool IsActive);
