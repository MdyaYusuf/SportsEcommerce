using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsEcommerce.Models.Entities;

namespace SportsEcommerce.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.ToTable("Categories").HasKey(c => c.Id);
    builder.Property(c => c.Id).HasColumnName("CategoryId");
    builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
    builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
    builder.Property(c => c.Name).HasColumnName("Name");

    builder
      .HasMany(c => c.Products)
      .WithOne(p => p.Category)
      .HasForeignKey(p => p.CategoryId)
      .OnDelete(DeleteBehavior.NoAction);
  }
}
