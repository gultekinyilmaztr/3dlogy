using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Caching;
using Base.Application.Pipelines.Logging;
using Base.Application.Requests;
using Base.Application.Responses;
using Base.Persistence.Paging;
using Domain.Entites;
using MediatR;

namespace Application.Features.SubCategories.Queries.GetList
{
    public class GetListSubCategoryQuery : IRequest<GetListResponse<GetListSubCategoryListItemDto>>, ICachableRequest, ILoggableRequest
    {
        public PageRequest PageRequest { get; set; }

        public string CacheKey => $"GetListSubCategoryQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

        public bool BypassCache { get; }
        public TimeSpan? SlidingExpiration { get; }

        public string? CacheGroupKey => "GetSubCategories";

        public class GetListSubCategoryQueryHandler : IRequestHandler<GetListSubCategoryQuery, GetListResponse<GetListSubCategoryListItemDto>>
        {
            private readonly ISubCategoryRepository _subCategoryRepository;
            private readonly IMapper _mapper;

            public GetListSubCategoryQueryHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
            {
                _subCategoryRepository = subCategoryRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListSubCategoryListItemDto>> Handle(GetListSubCategoryQuery request, CancellationToken cancellationToken)
            {
                Paginate<SubCategory> subCategories = await _subCategoryRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    withDeleted: true
                    );

                GetListResponse<GetListSubCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListSubCategoryListItemDto>>(subCategories);
                return response;

            }
        }
    }
}

