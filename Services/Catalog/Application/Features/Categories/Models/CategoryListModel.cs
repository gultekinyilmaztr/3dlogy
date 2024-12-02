using Application.Features.Categories.Dtos;
using Base.Persistence.Paging;

namespace Application.Features.Categories.Models
{
    public class CategoryListModel : BasePageableModel
    {
        public IList<CategoryListDto> Items { get; set; }
    }
}
