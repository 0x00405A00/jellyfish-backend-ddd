using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Presentation.Swagger.OperationFilter
{
    public class AddAuthorizationHeaderOperationFilter : IOperationFilter
    {
        public string HeaderName { get; }
        public string HeaderValue { get; }
        private readonly List<EndpointDataSource> _endpoints;
        public AddAuthorizationHeaderOperationFilter(string headerName,string headerValue,IEnumerable<EndpointDataSource> endpointSources)
        {
            _endpoints = endpointSources.ToList();
            HeaderName = headerName;
            HeaderValue = headerValue;
        }


        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var endpointAuthAttrs = context.MethodInfo.GetCustomAttributes<AuthorizeAttribute>();
                var nonAuthhAttrs = context.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>();
                if (endpointAuthAttrs == null || endpointAuthAttrs.Count() == 0)
                {
                    endpointAuthAttrs = descriptor.ControllerTypeInfo.GetCustomAttributes<AuthorizeAttribute>();
                }
                if (endpointAuthAttrs != null && !nonAuthhAttrs.Any())
                {
                    foreach (var endpointAuthAttr in endpointAuthAttrs)
                    {
                        string endpointAuthDesc = "Authorization Token for HTTP Requests.";
                        if (!string.IsNullOrEmpty(endpointAuthAttr.Policy))
                        {
                            endpointAuthDesc += "\nApplied Policy '" + endpointAuthAttr.Policy + "'";
                        }
                        if (!string.IsNullOrEmpty(endpointAuthAttr.Roles))
                        {
                            endpointAuthDesc += "\nApplied Roles '" + endpointAuthAttr.Roles + "'";
                        }
                        if (!string.IsNullOrEmpty(endpointAuthAttr.AuthenticationSchemes))
                        {
                            endpointAuthDesc += "\nApplied Authentication Schemes '" + endpointAuthAttr + "'";
                        }
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Description = endpointAuthDesc,
                            Required = true,
                            Schema = new OpenApiSchema
                            {
                                Type = "string",
                                Default = new OpenApiString(HeaderValue)
                            }
                        });
                    }
                }
            }
        }
    }
}
