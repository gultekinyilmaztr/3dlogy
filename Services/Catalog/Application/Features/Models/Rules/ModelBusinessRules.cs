using Application.Features.Models.Constants;
using Application.Services.Repositories;
using Base.Application.Rules;
using Base.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entites;

namespace Application.Features.Models.Rules
{
    public class ModelBusinessRules : BaseBusinessRules
    {
        private readonly IModelRepository _modelRepository;

        public ModelBusinessRules(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task ModelNameCannotBeDuplicatedWhenInserted(string name)//model ismin tekrar edemez.
        {
            Model? result = await _modelRepository.GetAsync(predicate: b => b.Name.ToLower() == name.ToLower());

            if (result != null)
            {
                throw new BusinessException(ModelsMessages.ModelNameExists);
            }
        }
    }
}
