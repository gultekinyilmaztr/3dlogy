using Base.Persistence.Repositories;

namespace Domain.Entites
{
    public class Category : BaseEntity<Guid>
    {
        public string CategoryName { get; set; }
        public string CategoryPhotoUrl { get; set; }
        public string IconUrl { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

    }


}

