using Application.Features.SubCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Caching;
using Base.Application.Pipelines.Logging;
using Base.Application.Pipelines.Transaction;
using Domain.Entites;
using MediatR;

namespace Application.Features.SubCategories.Commands.Create
{
    public class CreateSubCategoryCommand : IRequest<CreatedSubCategoryResponse>, ITransactionalRequest, ICacheRemoverRequest, ILoggableRequest
    {
        public string SubCategoryName { get; set; }
        public string IconUrl { get; set; }
        public string CategoryName { get; set; }

        public string CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "GetSubCategories";

        public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, CreatedSubCategoryResponse>
        {
            private readonly ISubCategoryRepository _subCategoryRepository;
            private readonly IMapper _mapper;
            private readonly SubCategoryBusinessRules _subCategoryBusinessRules;

            public CreateSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper, SubCategoryBusinessRules subCategoryBusinessRules)
            {
                _subCategoryRepository = subCategoryRepository;
                _mapper = mapper;
                _subCategoryBusinessRules = subCategoryBusinessRules;
            }

            public async Task<CreatedSubCategoryResponse>? Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
            {

                await _subCategoryBusinessRules.SubCategoryNameCannotBeDuplicatedWhenInserted(request.SubCategoryName);

                SubCategory subCategory = _mapper.Map<SubCategory>(request);
                subCategory.Id = Guid.NewGuid();

                await _subCategoryRepository.AddAsync(subCategory);
                //await _SubCategoryRepository.AddAsync(SubCategory2);

                CreatedSubCategoryResponse createdSubCategoryResponse = _mapper.Map<CreatedSubCategoryResponse>(subCategory);
                return createdSubCategoryResponse;
            }
        }
    }
}
