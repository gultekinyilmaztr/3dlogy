using AutoMapper;
using Contracts.Product;
using Elastic.Clients.Elasticsearch;
using Filter.WebApi.Models;
using MassTransit;

namespace Filter.WebApi.Consumers
{
    public class ProductCreatedConsumer : IConsumer<CreatedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ElasticsearchClient _elasticClient;
        private readonly ILogger<ProductCreatedConsumer> _logger;
        private string _indexName;

        public ProductCreatedConsumer(IMapper mapper, ElasticsearchClient elasticsearchClient, ILogger<ProductCreatedConsumer> logger, IConfiguration configuration)
        {
            _mapper = mapper;
            _elasticClient = elasticsearchClient;
            _indexName = configuration["Elasticsearch:Index"] ?? "";
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreatedProductResponse> context)
        {
            try
            {
                var product = _mapper.Map<Product>(context.Message);
                product.Id = context.Message.Id;

                await _elasticClient.IndexAsync(product, x => x.Index(_indexName).Id(context.Message.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while consuming the message");
                throw new Exception("An error occurred while processing the message", ex);
            }
        }
    }
}

