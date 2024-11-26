namespace Domain.Entites
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<int> SubCategoryIds { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public bool? IsDiscounted { get; set; }
        public double? DiscountRate { get; set; }
        public DateTime? DiscountEndDate { get; set; }

    }
}
