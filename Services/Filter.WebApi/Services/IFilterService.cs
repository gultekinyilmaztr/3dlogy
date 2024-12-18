using Filter.WebApi.Helper;
using Filter.WebApi.Models;

namespace Filter.WebApi.Services
{
    public interface IFilterService
    {
        Task<List<Product>> ProductFilterAsync(ProductFilter productFilter);
    }
}
