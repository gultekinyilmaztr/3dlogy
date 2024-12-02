namespace Application.Features.Categories.Queries.GetList
{
    public class GetListCategoryListItemDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPhotoUrl { get; set; }
        public string IconUrl { get; set; }
    }
}
