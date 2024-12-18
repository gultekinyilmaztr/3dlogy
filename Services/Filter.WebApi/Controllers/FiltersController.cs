using Filter.WebApi.Helper;
using Filter.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Filter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IFilterService _filterService;

        public FiltersController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpPost]
        public async Task<IActionResult> FilterProductsAsync(ProductFilter productFilter)
        {
            var response = await _filterService.ProductFilterAsync(productFilter);
            return Ok(response);
        }
    }
}
