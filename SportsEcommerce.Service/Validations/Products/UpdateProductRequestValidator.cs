using FluentValidation;
using SportsEcommerce.Models.Dtos.Products.Requests;

namespace SportsEcommerce.Service.Validations.Products;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
  public UpdateProductRequestValidator()
  {
    RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Ürün Id'si boş bırakılamaz.");

    RuleFor(p => p.Name)
        .NotEmpty().WithMessage("Ürün ismi boş bırakılamaz.")
        .Length(2, 100).WithMessage("Ürün ismi en az 2, en fazla 100 karakter olabilir.");

    RuleFor(p => p.Description)
        .NotEmpty().WithMessage("Ürün açıklaması boş bırakılamaz.")
        .MaximumLength(500).WithMessage("Ürün açıklaması en fazla 500 karakter olabilir.");

    RuleFor(p => p.ImageUrl)
        .NotEmpty().WithMessage("Ürün görseli boş bırakılamaz.")
        .Must(url => IsValidImageUrl(url)).WithMessage("Görsel geçerli bir resim türünde olmalıdır.");

    RuleFor(p => p.Price)
        .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır.");

    RuleFor(p => p.Stock)
        .GreaterThanOrEqualTo(0).WithMessage("Ürün stoğu negatif olamaz.");

    RuleFor(p => p.IsActive)
        .NotNull().WithMessage("Ürünün aktiflik durumu belirtilmelidir.");
  }

  private bool IsValidImageUrl(string url)
  {
    string[] validExtensions = { ".jpg", ".jpeg", ".png" };
    return validExtensions.Any(ext => url.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
  }
}
