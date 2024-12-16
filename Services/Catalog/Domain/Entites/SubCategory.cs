using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class SubCategory : BaseEntity<Guid>
    {
        public string SubCategoryName { get; set; }
        public string IconUrl { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
