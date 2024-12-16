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
            builder.Property(b => b.Description).HasColumnName("Description");
            builder.Property(b => b.Price).HasColumnName("Price").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(b => b.StockQuantity).HasColumnName("StockQuantity").IsRequired();
            builder.Property(b => b.IsNew).HasColumnName("IsNew").IsRequired();
            builder.Property(b => b.IsAvailable).HasColumnName("IsAvailable").IsRequired();
            builder.Property(b => b.Condition).HasColumnName("Condition").HasMaxLength(50);
            builder.Property(b => b.SubCategoryId).HasColumnName("SubCategoryId").IsRequired(false);
            builder.Property(b => b.Manufacturer).HasColumnName("Manufacturer").HasMaxLength(100);
            builder.Property(b => b.IsFeatured).HasColumnName("IsFeatured").IsRequired();
            builder.Property(b => b.DiscountPrice).HasColumnName("DiscountPrice").HasColumnType("decimal(18,2)");
            builder.Property(b => b.ViewCount).HasColumnName("ViewCount").IsRequired();
            builder.Property(b => b.BrandId).HasColumnName("BrandId");
            builder.Property(b => b.ModelId).HasColumnName("ModelId");

            builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

            // SubCategory ile olan ilişki
            builder.HasOne(b => b.SubCategory)
                .WithMany(s => s.Products)
                .HasForeignKey(b => b.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // SubCategory silindiğinde, bağlı Products silinsin.

            // Brand ile olan ilişki
            builder.HasOne(b => b.Brand)
                .WithMany()
                .HasForeignKey(b => b.BrandId)
                .OnDelete(DeleteBehavior.SetNull);  // Brand silindiğinde, Product'dan Brand bilgisi null olur, ancak silinmez.

            // Model ile olan ilişki
            builder.HasOne(b => b.Model)
                .WithMany()
                .HasForeignKey(b => b.ModelId)
                .OnDelete(DeleteBehavior.SetNull);  // Model silindiğinde, Product'tan Model bilgisi null olur, ancak silinmez.

            // ProductDetail ile olan ilişki
            builder.HasOne(b => b.ProductDetail)
                .WithOne(d => d.Product)
                .HasForeignKey<ProductDetail>(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);  // Product silindiğinde, ProductDetail de silinsin.

            // ProductImages ile olan ilişki
            builder.HasMany(b => b.ProductImages)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);  // Product silindiğinde, ProductImages da silinsin.

            // ProductTags ile olan ilişki
            builder.HasMany(b => b.ProductTags)
                .WithOne(pt => pt.Product)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Cascade);  // Product silindiğinde, ProductTags da silinsin.

            // Silinmiş verileri filtrelemek için
            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
        }
    }
}
