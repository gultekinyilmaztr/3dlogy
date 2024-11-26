using Application.Features.Categories.Commands.Create;
using MediatR;

namespace Application.Features.Categories.Handler
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {
        public Task<CreatedCategoryResponse>? Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            CreatedCategoryResponse createdCategoryResponse = new CreatedCategoryResponse();
            createdCategoryResponse.CategoryName = request.CategoryName;
            createdCategoryResponse.CategoryID = new int();
            return null;
        }
    }
}
