using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Presentation.Swagger.SchemaFilter
{
    public class YourSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Hier können Sie Ihr Schema anwenden
            schema.Properties.Add("property1", new OpenApiSchema { Type = "string" });
            schema.Properties.Add("property2", new OpenApiSchema { Type = "integer" });
        }
    }
}
