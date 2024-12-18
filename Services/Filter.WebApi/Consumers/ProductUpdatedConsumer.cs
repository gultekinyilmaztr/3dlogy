using AutoMapper;
using Contracts.Product;
using Elastic.Clients.Elasticsearch;
using Filter.WebApi.Models;
using MassTransit;

namespace Filter.WebApi.Consumers
{
    public class ProductUpdatedConsumer : IConsumer<UpdatedProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly ElasticsearchClient _elasticClient;
        private readonly ILogger<ProductUpdatedConsumer> _logger;
        private string _indexName;

        public ProductUpdatedConsumer(IMapper mapper, ElasticsearchClient elasticClient, ILogger<ProductUpdatedConsumer> logger, IConfiguration configuration)
        {
            _mapper = mapper;
            _elasticClient = elasticClient;
            _indexName = configuration["Elasticsearch:Index"] ?? "";
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UpdatedProductResponse> context)
        {
            try
            {
                var product = _mapper.Map<Product>(context.Message);

                var response = await _elasticClient.UpdateAsync<Product, object>
                    (context.Message.Id, x => x.Index(_indexName).Doc(product).DocAsUpsert(true));

                if (!response.IsValidResponse)
                {
                    throw new Exception("An error occurred while updating the Product");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while consuming the message");
                throw new Exception("An error occurred while processing the message", ex);
            }
        }
    }
}

