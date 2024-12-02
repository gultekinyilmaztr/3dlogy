using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class SubCategory : BaseEntity<Guid>
    {
        public string SubCategoryName { get; set; }
        public string PhotoUrl { get; set; }
        public string IconUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
