using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Presentation.Swagger.Attribute
{
    public sealed class SwaggerResponseExampleAttributeExtended : SwaggerResponseExampleAttribute
    {
        private SwaggerResponseExampleAttributeExtended(int statusCode, Type examplesProviderType) : base(statusCode, examplesProviderType)
        {
        }
        public SwaggerResponseExampleAttributeExtended(HttpStatusCode statusCode, Type examplesProviderType):this((int)statusCode, examplesProviderType)
        {

        }
    }
}
