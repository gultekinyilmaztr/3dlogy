using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Base.Application.Rules;
using Base.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entites;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandNameCannotBeDuplicatedWhenInserted(string name)//marka ismin tekrar edemez.
    {
        Brand? result = await _brandRepository.GetAsync(predicate: b => b.Name.ToLower() == name.ToLower());

        if (result != null)
        {
            throw new BusinessException(BrandsMessages.BrandNameExists);
        }
    }
}
