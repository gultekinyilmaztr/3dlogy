namespace Application.Features.Categories.Commands.Update
{
    public class UpdatedCategoryResponse
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPhotoUrl { get; set; }
        public string IconUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
