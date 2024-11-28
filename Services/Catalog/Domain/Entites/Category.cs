using Base.Persistence.Services.Repositories;
using System.Xml.Linq;

namespace Domain.Entites
{
    public class Category : BaseEntity<Guid>
    {
        public string CategoryName { get; set; }
        public string CategoryPhotoUrl { get; set; }
        public string IconUrl { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }

    }


}

