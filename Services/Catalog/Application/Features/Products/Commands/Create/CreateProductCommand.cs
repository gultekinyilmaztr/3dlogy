using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Caching;
using Base.Application.Pipelines.Logging;
using Base.Application.Pipelines.Transaction;
using Domain.Entites;
using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<CreatedProductResponse>, ITransactionalRequest, ICacheRemoverRequest, ILoggableRequest
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
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SubCategoryName { get; set; }

        public string? CacheKey => "";

        public bool BypassCache => false;

        public string? CacheGroupKey => "GetProducts";

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            private readonly ProductBusinessRules _productBusinessRules;

            public CreateProductCommandHandler(IProductRepository ProductRepository, IMapper mapper, ProductBusinessRules ProductBusinessRules)
            {
                _productRepository = ProductRepository;
                _mapper = mapper;
                _productBusinessRules = ProductBusinessRules;
            }

            public async Task<CreatedProductResponse>? Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {

                //await _productBusinessRules.ProductNameCannotBeDuplicatedWhenInserted(request.Name);

                Product Product = _mapper.Map<Product>(request);
                Product.Id = Guid.NewGuid();

                await _productRepository.AddAsync(Product);

                CreatedProductResponse createdProductResponse = _mapper.Map<CreatedProductResponse>(Product);
                return createdProductResponse;
            }
        }
    }
}
