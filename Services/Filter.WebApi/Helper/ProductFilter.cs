using System.Text.Json.Serialization;

namespace Filter.WebApi.Helper
{
    public class ProductFilter
    {
        [JsonPropertyName("productName")]
        public string Name { get; set; }
        [JsonPropertyName("brand")]
        public List<string> Brand { get; set; }
        [JsonPropertyName("model")]
        public List<string> Model { get; set; }
        [JsonPropertyName("category")]
        public List<string> SubCategory { get; set; }
    }
}
