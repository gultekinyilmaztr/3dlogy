using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class Tag : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

    }
}
