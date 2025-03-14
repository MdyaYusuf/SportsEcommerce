using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.ToTable("Orders").HasKey(o => o.Id);
    builder.Property(o => o.Id).HasColumnName("OrderId");
    builder.Property(o => o.OrderDate).HasColumnName("OrderDate");
    builder.Property(o => o.Total).HasColumnName("Total");
    builder.Property(o => o.Adress).HasColumnName("Adress");
    builder.Property(o => o.OrderDetails).HasColumnName("OrderDetails");
    builder.Property(o => o.UserId).HasColumnName("UserId");

    builder
      .HasOne(o => o.User)
      .WithMany(u => u.Orders)
      .HasForeignKey(o => o.UserId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
