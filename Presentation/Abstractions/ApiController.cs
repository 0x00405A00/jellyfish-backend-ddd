using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Abstractions
{
    [ApiController]
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
            var tmp = HttpContextAccessor.HttpContext.Request.Headers.Authorization;
        }

    }
}
