using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.PhotoUrl).HasColumnName("PhotoUrl").IsRequired();
            builder.Property(b => b.Summary).HasColumnName("Summary").IsRequired();
            builder.Property(b => b.SubCategoryIds).HasColumnName("SubCategoryIds").IsRequired();
            builder.Property(b => b.Price).HasColumnName("Price").IsRequired();
            builder.Property(b => b.DiscountRate).HasColumnName("DiscountRate");
            builder.Property(b => b.DiscountEndDate).HasColumnName("DiscountEndDate");
            builder.Property(b => b.ProductState).HasColumnName("ProductState");


            builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
        }
    }
}

