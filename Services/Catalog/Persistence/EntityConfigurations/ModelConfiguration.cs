using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Models").HasKey(m => m.Id);

            builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
            builder.Property(m => m.Name).HasColumnName("Name").IsRequired();
            builder.Property(m => m.ImageUrl).HasColumnName("ImageUrl").IsRequired();

            builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(m => m.Name).IsUnique();

            builder.HasOne(m => m.Brand)
                   .WithMany(b => b.Models)
                   .HasForeignKey(m => m.BrandId);

            builder.HasQueryFilter(m => !m.DeletedDate.HasValue);
        }
    }

}


