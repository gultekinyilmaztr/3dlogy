using Application.Services.Repositories;

namespace Domain.Entites
{
    public class ProductDetail : BaseEntity<ProductDetail>
    {
        public string Title { get; set; }
        public List<string> Pictures { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public Guid ProductId { get; set; }

    }
}
