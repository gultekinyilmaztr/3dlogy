using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class ProductDetail : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string AdditionalInfo { get; set; }
        public string ImageURL { get; set; }
        public string VideoURL { get; set; }


    }
}
