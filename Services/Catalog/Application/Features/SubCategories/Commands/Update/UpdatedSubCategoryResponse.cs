namespace Application.Features.SubCategories.Commands.Update
{
    public class UpdatedSubCategoryResponse
    {
        public Guid Id { get; set; }
        public string SubCategoryName { get; set; }
        public string IconUrl { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
