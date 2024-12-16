using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Caching;
using Domain.Entites;
using MediatR;

namespace Application.Features.SubCategories.Commands.Delete
{
    public class DeleteSubCategoryCommand : IRequest<DeletedSubCategoryResponse>, ICacheRemoverRequest
    {
        public Guid Id { get; set; }

        public string CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "GetSubCategories";

        public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, DeletedSubCategoryResponse>
        {
            private readonly ISubCategoryRepository _subCategoryRepository;
            private readonly IMapper _mapper;

            public DeleteSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
            {
                _subCategoryRepository = subCategoryRepository;
                _mapper = mapper;
            }
            public async Task<DeletedSubCategoryResponse> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
            {
                SubCategory? subCategory = await _subCategoryRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);



                await _subCategoryRepository.DeleteAsync(subCategory);

                DeletedSubCategoryResponse response = _mapper.Map<DeletedSubCategoryResponse>(subCategory);

                return response;
            }
        }
    }
}
