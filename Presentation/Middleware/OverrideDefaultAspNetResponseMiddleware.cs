using Microsoft.AspNetCore.Mvc;
using Presentation.Extension;
using System.Net;

namespace Presentation.Middleware
{
    public class OverrideDefaultAspNetResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public OverrideDefaultAspNetResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalStream = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context);

                HttpStatusCode statusCode = (HttpStatusCode)context.Response.StatusCode;
                if(statusCode.IsClientError())
                {
                    Nullable<(string title, string detail)> responseOverride = null;
                    switch(statusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            responseOverride = ("Unauthorized", "You are not authorized");
                            break;
                        case HttpStatusCode.BadRequest:
                            responseOverride = ("Bad request", "Your request is bad");
                            break;
                        default:
                            if(context.Response.ContentLength is null || context.Response.ContentLength == 0)
                            {
                                responseOverride = (String.Empty,String.Empty);
                            }
                            break;
                    }
                    if(responseOverride is not null)
                    {
                        var objectResult = JsonApiResultExtension.ErrorByHttpStatusCode(responseOverride.Value.title, responseOverride.Value.detail, statusCode);

                        await objectResult.ExecuteResultAsync(new ActionContext
                        {
                            HttpContext = context
                        });
                    }
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalStream);
            }
        }
    }
    public static class OverrideDefaultAspNetResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseOverrideDefaultAspNetResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OverrideDefaultAspNetResponseMiddleware>();
        }
    }
}
