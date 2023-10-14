using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Cryptography.Xml;

namespace Presentation.Swagger.OperationFilter
{
    public class CategorizeFilter : IOperationFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiOperation operation, OperationFilterContext context)
        {
            string path = context.ApiDescription.RelativePath;
            string segment = path.Split('/')[1];

            if (segment != context.ApiDescription.GroupName)
            {
                operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name=segment } };
            }
        }
    }
}
