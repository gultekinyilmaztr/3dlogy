using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Filter.WebApi.Helper;
using Filter.WebApi.Models;

namespace Filter.WebApi.Services
{
    public class FilterService : IFilterService
    {
        private readonly ElasticsearchClient _elasticClient;
        private string _indexName;

        public FilterService(ElasticsearchClient elasticClient, IConfiguration configuration)
        {
            _elasticClient = elasticClient;
            _indexName = configuration["Elasticsearch:Index"] ?? "";
        }

        public async Task<List<Product>> ProductFilterAsync(ProductFilter productFilter)
        {
            var queries = new List<Query>();

            if (!string.IsNullOrEmpty(productFilter.Name))
            {
                queries.Add(new MatchQuery(new Field("productName")) { Query = productFilter.Name });
            }

            if (productFilter.Brand != null && productFilter.Brand.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                queries.Add(new BoolQuery
                {
                    Should = productFilter.Brand
                        .Where(brand => !string.IsNullOrWhiteSpace(brand))
                        .Select(brand => (Query)new MatchQuery(new Field("brand")) { Query = brand })
                        .ToList(),
                    MinimumShouldMatch = 1
                });
            }

            if (productFilter.Model != null && productFilter.Model.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                queries.Add(new BoolQuery
                {
                    Should = productFilter.Model
                        .Where(model => !string.IsNullOrWhiteSpace(model))
                        .Select(model => (Query)new MatchQuery(new Field("model")) { Query = model })
                        .ToList(),
                    MinimumShouldMatch = 1
                });
            }

            if (productFilter.SubCategory != null && productFilter.SubCategory.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                queries.Add(new BoolQuery
                {
                    Should = productFilter.SubCategory
                        .Where(subCategory => !string.IsNullOrWhiteSpace(subCategory))
                        .Select(subCategory => (Query)new MatchQuery(new Field("subCategory")) { Query = subCategory })
                        .ToList(),
                    MinimumShouldMatch = 1
                });
            }

            var response = await _elasticClient.SearchAsync<Product>
                (x => x.Index(_indexName).Query(x => x.Bool(x => x.Must(queries.ToArray()))));

            if (!response.IsValidResponse)
            {
                throw new Exception("An error occurred while filtering Products");
            }

            return response.Documents.ToList();
        }


    }
}
