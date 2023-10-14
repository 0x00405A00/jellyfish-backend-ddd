using Presentation.Swagger.Attribute;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Presentation.Swagger.OperationFilter
{
    public class OpenApiIgnoreMethodParameterOperationFilter : IOperationFilter
    {
        private readonly List<EndpointDataSource> _endpoints;
        public OpenApiIgnoreMethodParameterOperationFilter()
        {
        }
        public void Apply(Microsoft.OpenApi.Models.OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null || context == null || context.ApiDescription?.ParameterDescriptions == null)
                return;

            var parametersToHide = context.ApiDescription.ParameterDescriptions
                .Where(parameterDescription => ParameterHasIgnoreAttribute(parameterDescription))
                .ToList();

            if (parametersToHide.Count == 0)
                return;

            foreach (var parameterToHide in parametersToHide)
            {
                var parameter = operation.Parameters.FirstOrDefault(parameter => string.Equals(parameter.Name, parameterToHide.Name, StringComparison.Ordinal));
                if (parameter != null)
                    operation.Parameters.Remove(parameter);
            }
        }

        private static bool ParameterHasIgnoreAttribute(Microsoft.AspNetCore.Mvc.ApiExplorer.ApiParameterDescription parameterDescription)
        {
            if (parameterDescription.ModelMetadata is Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadata metadata)
            {
                return metadata.Attributes.ParameterAttributes.Any(attribute => attribute.GetType() == typeof(OpenApiIgnoreMethodParameter));
            }

            return false;
        }
    }
}
