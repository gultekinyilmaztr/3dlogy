namespace Application.Features.SubCategories.Queries.GetById
{
    public class GetByIdSubCategoryResponse
    {
        public Guid Id { get; set; }
        public string SubCategoryName { get; set; }
        public string IconUrl { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
