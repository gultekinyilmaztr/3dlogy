using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Caching;
using Contracts.Product;
using Domain.Entites;
using MediatR;

namespace Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<UpdatedProductResponse>, ICacheRemoverRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsNew { get; set; }
        public bool IsAvailable { get; set; }
        public string Condition { get; set; }
        public string Manufacturer { get; set; }
        public bool IsFeatured { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int ViewCount { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? ModelId { get; set; }
        public Guid? SubCategoryId { get; set; }

        public string? CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "GetProducts";

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                Product? product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

                product = _mapper.Map(request, product);

                await _productRepository.UpdateAsync(product);

                UpdatedProductResponse response = _mapper.Map<UpdatedProductResponse>(product);

                return response;
            }
        }
    }
}
