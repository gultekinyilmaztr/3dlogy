using Application.Features.Categories.Constants;
using Application.Services.Repositories;
using Base.Application.Rules;
using Base.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entites;

namespace Application.Features.Categories.Rules
{
    public class CategoryBusinessRules : BaseBusinessRules
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryBusinessRules(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CategoryNameCannotBeDuplicatedWhenInserted(string name)
        {
            Category? result = await _categoryRepository.GetAsync(predicate: b => b.CategoryName.ToLower() == name.ToLower());

            if (result != null)
            {
                throw new BusinessException(CategoriesMessages.BrandNameExists);
            }
        }
    }
}
