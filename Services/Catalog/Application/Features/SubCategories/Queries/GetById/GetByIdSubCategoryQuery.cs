using Application.Services.Repositories;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Features.SubCategories.Queries.GetById
{
    public class GetByIdSubCategoryQuery : IRequest<GetByIdSubCategoryResponse>
    {
        public Guid Id { get; set; }

        public class GetByIdSubCategoryQueryHandler : IRequestHandler<GetByIdSubCategoryQuery, GetByIdSubCategoryResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISubCategoryRepository _subCategoryRepository;

            public GetByIdSubCategoryQueryHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository)
            {
                _mapper = mapper;
                _subCategoryRepository = subCategoryRepository;
            }

            public async Task<GetByIdSubCategoryResponse> Handle(GetByIdSubCategoryQuery request, CancellationToken cancellationToken)
            {
                SubCategory? subCategory = await _subCategoryRepository.GetAsync(predicate: b => b.Id == request.Id, withDeleted: true, cancellationToken: cancellationToken);

                GetByIdSubCategoryResponse response = _mapper.Map<GetByIdSubCategoryResponse>(subCategory);

                return response;
            }
        }
    }
}
