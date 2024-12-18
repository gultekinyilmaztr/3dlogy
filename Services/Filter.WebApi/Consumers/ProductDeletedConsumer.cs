using Contracts.Product;
using Elastic.Clients.Elasticsearch;
using Filter.WebApi.Models;
using MassTransit;

namespace Filter.WebApi.Consumers
{
    public class ProductDeletedConsumer : IConsumer<DeletedProductResponse>
    {
        private readonly ElasticsearchClient _elasticClient;
        private readonly ILogger<ProductDeletedConsumer> _logger;
        private string _indexName;

        public ProductDeletedConsumer(ElasticsearchClient elasticClient, ILogger<ProductDeletedConsumer> logger, IConfiguration configuration)
        {
            _elasticClient = elasticClient;
            _indexName = configuration["Elasticsearch:Index"] ?? "";
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DeletedProductResponse> context)
        {
            try
            {
                var response = await _elasticClient.DeleteAsync<Product>(context.Message.Id, x => x.Index(_indexName));

                if (!response.IsValidResponse)
                {
                    throw new Exception("An error occurred while deleting the Product");
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

