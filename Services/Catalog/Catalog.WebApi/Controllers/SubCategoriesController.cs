using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Update;
using Application.Features.SubCategories.Queries.GetById;
using Application.Features.SubCategories.Queries.GetList;
using Base.Application.Requests;
using Base.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSubCategoryCommand createSubCategoryCommand)
        {
            CreatedSubCategoryResponse response = await Mediator.Send(createSubCategoryCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSubCategoryQuery getListSubCategoryQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListSubCategoryListItemDto> response = await Mediator.Send(getListSubCategoryQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdSubCategoryQuery getByIdSubCategoryQuery = new() { Id = id };
            GetByIdSubCategoryResponse response = await Mediator.Send(getByIdSubCategoryQuery);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSubCategoryCommand updateSubCategoryCommand)
        {
            UpdatedSubCategoryResponse response = await Mediator.Send(updateSubCategoryCommand);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeletedSubCategoryResponse response = await Mediator.Send(new DeleteSubCategoryCommand { Id = id });

            return Ok(response);
        }
    }
}
