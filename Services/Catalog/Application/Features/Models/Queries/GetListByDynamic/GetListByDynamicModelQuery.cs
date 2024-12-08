using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Requests;
using Base.Application.Responses;
using Base.Persistence.Dynamic;
using Base.Persistence.Paging;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListByDynamic
{
    public class GetListByDynamicModelQuery : IRequest<GetListResponse<GetListByDynamicModelListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }

        public class GetListByDynamicModelQueryHandler : IRequestHandler<GetListByDynamicModelQuery, GetListResponse<GetListByDynamicModelListItemDto>>
        {
            private readonly IModelRepository _modelRepository;
            private readonly IMapper _mapper;

            public GetListByDynamicModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;
            }
            public async Task<GetListResponse<GetListByDynamicModelListItemDto>> Handle(GetListByDynamicModelQuery request, CancellationToken cancellationToken)
            {
                Paginate<Model> models = await _modelRepository.GetListByDynamicsAsync(
                     request.DynamicQuery,
                     include: m => m.Include(m => m.Brand),
                     size: request.PageRequest.PageSize
                     );

                var response = _mapper.Map<GetListResponse<GetListByDynamicModelListItemDto>>(models);

                return response;


            }
        }
    }
}
