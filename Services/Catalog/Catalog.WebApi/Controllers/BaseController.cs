using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
