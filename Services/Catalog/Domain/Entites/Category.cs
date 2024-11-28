using Application.Services.Repositories;

namespace Domain.Entites
{
    public class Category : BaseEntity<Category>
    {
        public string CategoryName { get; set; }
        public string CategoryPhotoUrl { get; set; }
        public string IconUrl { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }


}

