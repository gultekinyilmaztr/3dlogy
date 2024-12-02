using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Requests;
using Base.Application.Responses;
using Base.Persistence.Paging;
using Domain.Entites;
using MediatR;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetListCategoryQuery : IRequest<GetListResponse<GetListCategoryListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public string CacheKey => $"GetListCategoryQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }

        public string? CacheGroupKey => "GetCategories";

        public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, GetListResponse<GetListCategoryListItemDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListCategoryListItemDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                Paginate<Category> categories = await _categoryRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    withDeleted: true
                    );

                GetListResponse<GetListCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListCategoryListItemDto>>(categories);
                return response;

            }
        }
    }
}
