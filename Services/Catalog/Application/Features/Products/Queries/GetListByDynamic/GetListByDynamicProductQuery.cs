using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Requests;
using Base.Application.Responses;
using Base.Persistence.Dynamic;
using Base.Persistence.Paging;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetListByDynamic
{
    public class GetListByDynamicProductQuery : IRequest<GetListResponse<GetListByDynamicProductListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }

        public class GetListByDynamicProductQueryHandler : IRequestHandler<GetListByDynamicProductQuery, GetListResponse<GetListByDynamicProductListItemDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetListByDynamicProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<GetListResponse<GetListByDynamicProductListItemDto>> Handle(GetListByDynamicProductQuery request, CancellationToken cancellationToken)
            {
                Paginate<Product> products = await _productRepository.GetListByDynamicsAsync(
                     request.DynamicQuery,
                     include: m => m.Include(m => m.Brand),
                     size: request.PageRequest.PageSize
                     );

                var response = _mapper.Map<GetListResponse<GetListByDynamicProductListItemDto>>(products);

                return response;


            }

        }
    }
}
