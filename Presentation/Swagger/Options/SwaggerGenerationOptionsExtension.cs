using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Presentation.Swagger.ExampleProvider.Extension;
using Shared.DataTransferObject.Abstraction;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Presentation.Swagger.Options
{
    public static class SwaggerGenerationOptionsExtension
    {
        public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
            IncludeFields = false
        };
        private static SwaggerGenOptions SetSwaggerCustomRequestBody(this SwaggerGenOptions swaggerGenOptions,Type type, JsonSerializerOptions jsonSerializerOptions = null)
        {
            var extensionType = typeof(Microsoft.Extensions.DependencyInjection.SwaggerGenOptionsExtensions);
            var methods = extensionType
                            .GetMethods().Where(x => x.Name == "MapType" && x.IsGenericMethod);


            var method = methods.First()
                             .MakeGenericMethod(type);
            var methodParams = method.GetParameters();


            var extensionTypeExampleProvider = typeof(DependencyInjection);
            var methodsExampleProvider = extensionTypeExampleProvider
                            .GetMethods().Where(x => x.Name == nameof(ExampleDataProviderExtension.GetExampleProviderObjects) && x.IsGenericMethod).Single();

            var methodExampleProvider = methodsExampleProvider
                                            .MakeGenericMethod(type);
            var exampleProvider = methodExampleProvider.Invoke(null, new object[] { });
            if (exampleProvider is null)
                return null;
            var exampleRequestMethod = exampleProvider.GetType().GetMethod("GetRequestExample");
            var exampleRequest = exampleRequestMethod.Invoke(exampleProvider, new object[] { });
            var exampleRequestJson = JsonSerializer.Serialize(exampleRequest, exampleRequest.GetType(), jsonSerializerOptions??JsonSerializerOptions);


            var schemaFunc = new Func<OpenApiSchema>(() => new Microsoft.OpenApi.Models.OpenApiSchema
            {
                Type = "object",
                Example = new OpenApiString(exampleRequestJson)
            });
            method.Invoke(swaggerGenOptions, new object[] { swaggerGenOptions, schemaFunc });
            return swaggerGenOptions;
        }
        public static SwaggerGenOptions GenerateCustomRequestBodyForApiDataTransferObject(this SwaggerGenOptions options)
        {
            var dtos = DataTransferObjectExtension.FindDataTransferObjects();
            foreach (var dto in dtos)
            {
                options.SetSwaggerCustomRequestBody(dto);
            }
            return options;
        }
    }
}
