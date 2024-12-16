using Application.Features.SubCategories.Constants;
using Application.Services.Repositories;
using Base.Application.Rules;
using Base.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entites;

namespace Application.Features.SubCategories.Rules
{
    public class SubCategoryBusinessRules : BaseBusinessRules
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryBusinessRules(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task SubCategoryNameCannotBeDuplicatedWhenInserted(string name)
        {
            SubCategory? result = await _subCategoryRepository.GetAsync(predicate: b => b.SubCategoryName.ToLower() == name.ToLower());

            if (result != null)
            {
                throw new BusinessException(SubCategoriesMessages.SubCategoryNameExists);
            }
        }
    }
}
