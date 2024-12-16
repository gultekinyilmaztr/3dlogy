using Application.Features.Categories.Commands.Create;
using FluentValidation;

namespace Application.Features.SubCategories.Commands.Create
{
    public class CreateSubCategoryCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateSubCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(3);
        }
    }
}
