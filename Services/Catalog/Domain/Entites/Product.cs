using Base.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entites
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public bool? IsDiscounted { get; set; }
        public double? DiscountRate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public ProductState ProductState { get; set; }
        public Guid SubCategoyId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

    }
}
