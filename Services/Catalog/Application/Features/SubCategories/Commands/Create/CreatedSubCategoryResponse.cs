namespace Application.Features.SubCategories.Commands.Create
{
    public class CreatedSubCategoryResponse
    {
        public Guid Id { get; set; }
        public string SubCategoryName { get; set; }
        public string IconUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
