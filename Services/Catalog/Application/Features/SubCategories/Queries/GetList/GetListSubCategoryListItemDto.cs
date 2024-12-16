namespace Application.Features.SubCategories.Queries.GetList
{
    public class GetListSubCategoryListItemDto
    {
        public Guid Id { get; set; }
        public string SubCategoryName { get; set; }
        public string IconUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
