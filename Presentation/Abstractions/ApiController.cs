using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataFilter.Presentation;
using System.Net.Mime;

namespace WebApi.Abstractions
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public abstract class ApiController: ControllerBase
    {
        protected readonly ISender Sender;
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected ApiController(
            ISender sender,
            IHttpContextAccessor httpContextAccessor)
        {
            Sender = sender;
            HttpContextAccessor = httpContextAccessor;
        }
        protected ApiController() 
        {

        }

    }

    public abstract class ApiController<T> : ApiController
    {

        protected ApiController(
            ISender sender,
            IHttpContextAccessor httpContextAccessor) : base(sender,httpContextAccessor)
        {

        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public abstract Task<IActionResult> Create([FromBody] T model, CancellationToken cancellationToken);

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public abstract Task<IActionResult> Read(Guid id, CancellationToken cancellationToken);

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public abstract Task<IActionResult> ReadAll([FromQuery] SearchParams? searchParamsFromQuery, [FromBody] SearchParamsBody? searchParamsFromBody, CancellationToken cancellationToken);

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public abstract Task<IActionResult> Update(Guid id, [FromBody] T model, CancellationToken cancellationToken);

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public abstract Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken);

    }
}
