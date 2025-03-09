using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
  public void Configure(EntityTypeBuilder<Cart> builder)
  {
    builder.ToTable("Carts").HasKey(c => c.Id);
    builder.Property(c => c.Id).HasColumnName("CartId");
    builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
    builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
    builder.Property(c => c.CustomerId).HasColumnName("CustomerId").IsRequired();

    builder
      .HasOne(c => c.Customer)
      .WithOne(u => u.ActiveCart)
      .HasForeignKey<Cart>(c => c.CustomerId)
      .OnDelete(DeleteBehavior.NoAction);

    builder
      .HasMany(c => c.CartItems)
      .WithOne(ci => ci.Cart)
      .HasForeignKey(ci => ci.CartId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
