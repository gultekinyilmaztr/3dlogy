using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class ProductTag : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
