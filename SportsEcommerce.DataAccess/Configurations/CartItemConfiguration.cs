using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
  public void Configure(EntityTypeBuilder<CartItem> builder)
  {
    builder.ToTable("CartItems").HasKey(ci => ci.Id);
    builder.Property(ci => ci.Id).HasColumnName("CartItemId");
    builder.Property(ci => ci.CreatedDate).HasColumnName("CreatedDate");
    builder.Property(ci => ci.UpdatedDate).HasColumnName("UpdatedDate");
    builder.Property(ci => ci.Quantity).HasColumnName("Quantity").IsRequired();
    builder.Property(ci => ci.CartId).HasColumnName("CartId").IsRequired();
    builder.Property(ci => ci.ProductId).HasColumnName("ProductId").IsRequired();

    builder
      .HasOne(ci => ci.Cart)
      .WithMany(c => c.CartItems)
      .HasForeignKey(ci => ci.CartId)
      .OnDelete(DeleteBehavior.Cascade);

    builder
      .HasOne(ci => ci.Product)
      .WithMany()
      .HasForeignKey(ci => ci.ProductId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
