using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal.TypeHandlers.DateTimeHandlers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new InvalidOperationException();

        private ILogger _logger;
        protected ILogger Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>() ?? throw new InvalidOperationException();
    }
}
