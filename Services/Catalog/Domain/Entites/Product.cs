using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsNew { get; set; }
        public bool IsAvailable { get; set; }
        public string Condition { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string Manufacturer { get; set; }
        public bool IsFeatured { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int ViewCount { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? ModelId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }


}
