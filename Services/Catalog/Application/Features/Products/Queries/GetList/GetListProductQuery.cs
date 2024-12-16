using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Caching;
using Base.Application.Pipelines.Logging;
using Base.Application.Requests;
using Base.Application.Responses;
using Base.Persistence.Paging;
using Domain.Entites;
using MediatR;

namespace Application.Features.Products.Queries.GetList
{
    public class GetListProductQuery : IRequest<GetListResponse<GetListProductListItemDto>>, ICachableRequest, ILoggableRequest
    {
        public PageRequest PageRequest { get; set; }
        public string CacheKey => $"GetListProductQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

        public bool BypassCache { get; }

        public TimeSpan? SlidingExpiration { get; }

        public string? CacheGroupKey => "GetProducts";

        public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, GetListResponse<GetListProductListItemDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetListProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListProductListItemDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                Paginate<Product> products = await _productRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    withDeleted: true
                    );

                GetListResponse<GetListProductListItemDto> response = _mapper.Map<GetListResponse<GetListProductListItemDto>>(products);
                return response;

            }
        }
    }
}
