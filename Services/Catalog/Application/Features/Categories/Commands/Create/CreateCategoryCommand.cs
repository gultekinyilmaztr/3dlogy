using MediatR;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<CreatedCategoryResponse>
    {
        public string CategoryName { get; set; }
    }
}
