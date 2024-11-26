namespace Domain.Entites
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPhotoUrl { get; set; }
        public string IconUrl { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}

