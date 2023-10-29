using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Abstractions
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public abstract class ApiController : ControllerBase
    {
        protected readonly string JwtToken;

        protected readonly ISender Sender;
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected ApiController(ISender sender, IHttpContextAccessor httpContextAccessor)
        {
            Sender = sender;
            HttpContextAccessor = httpContextAccessor;
            JwtToken = HttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization",
                                                               out var bearer) ? null : null;
        }
        protected ApiController() 
        {

        }

    }
}
