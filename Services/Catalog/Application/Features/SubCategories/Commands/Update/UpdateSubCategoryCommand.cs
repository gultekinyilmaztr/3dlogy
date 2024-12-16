using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Caching;
using Domain.Entites;
using MediatR;

namespace Application.Features.SubCategories.Commands.Update
{
    public class UpdateSubCategoryCommand : IRequest<UpdatedSubCategoryResponse>, ICacheRemoverRequest
    {
        public Guid Id { get; set; }
        public string SubCategoryName { get; set; }
        public string IconUrl { get; set; }
        public string CategoryName { get; set; }

        public string CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "GetSubCategories";

        public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand, UpdatedSubCategoryResponse>
        {
            private readonly ISubCategoryRepository _subCategoryRepository;
            private readonly IMapper _mapper;

            public UpdateSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
            {
                _subCategoryRepository = subCategoryRepository;
                _mapper = mapper;
            }
            public async Task<UpdatedSubCategoryResponse> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
            {
                SubCategory? subCategory = await _subCategoryRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

                subCategory = _mapper.Map(request, subCategory);

                await _subCategoryRepository.UpdateAsync(subCategory);

                UpdatedSubCategoryResponse response = _mapper.Map<UpdatedSubCategoryResponse>(subCategory);

                return response;
            }
        }
    }
}
