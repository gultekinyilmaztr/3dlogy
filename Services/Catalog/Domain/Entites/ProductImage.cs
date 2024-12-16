using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class ProductImage : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public int Order { get; set; }
        public DateTime UploadedDate { get; set; }
        public bool IsMainImage { get; set; }
    }
}
