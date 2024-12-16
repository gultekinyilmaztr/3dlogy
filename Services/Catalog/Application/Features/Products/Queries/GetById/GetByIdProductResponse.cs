namespace Application.Features.Products.Queries.GetById
{
    public class GetByIdProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsNew { get; set; }
        public bool IsAvailable { get; set; }
        public string Condition { get; set; }
        public string Manufacturer { get; set; }
        public bool IsFeatured { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int ViewCount { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
