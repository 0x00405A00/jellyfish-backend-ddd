using AutoMapper.Internal;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Presentation.Swagger.Options;
using Shared.ApiDataTransferObject;
using Shared.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Presentation.Swagger.DocumentFilter
{
    public class PolymorphismDocumentFilter<T> : IDocumentFilter
    {

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            RegisterSubClasses(swaggerDoc,context, typeof(T));
        }

        private static void RegisterSubClasses(OpenApiDocument swaggerDoc, DocumentFilterContext context, Type abstractType)
        {
            const string discriminatorName = "$type";
            var schemaRepository = context.SchemaRepository.Schemas;
            var schemaGenerator = context.SchemaGenerator;
            string jsonStrExample = null;
            string abstractSchemaId = abstractType.IsGenericType?($"{abstractType.GetNameWithoutGenericArity()}<{abstractType.GenericArgumentsConcatToString()}>"):(abstractType.Name);

            Type[] subTypes = abstractType.GenericTypeArguments;

            foreach (var subType in subTypes)
            {
                var schemaId = subType.Name;

                if (!schemaRepository.ContainsKey(schemaId))
                {
                    var schema = schemaGenerator.GenerateSchema(subType, context.SchemaRepository);
                    schemaRepository.Add(schemaId, schema);
                }
            }

            // Set up polymorphism
            var parentSchema = new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                    {
                        { discriminatorName, new OpenApiSchema { Type = "string" } }
                    },
                Discriminator = new OpenApiDiscriminator
                {
                    PropertyName = discriminatorName,
                    Mapping = subTypes.ToDictionary(t => t.Name, t => t.Name)
                }
            };

            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(ApiDataTransferObject<>) && abstractType.GenericTypeArguments.Length == 1)
            {
                // Add the parent schema to the repository
                abstractSchemaId = nameof(ApiDataTransferObject<T>)+"<" + abstractType.GenericTypeArguments.First().Name+">";
                schemaRepository[abstractSchemaId] = parentSchema;

                var genericSubType = abstractType.GenericTypeArguments.First();
                var genericTypeInstance = Activator.CreateInstance(genericSubType);
                var method = typeof(ApiDataTransferObjectExtension)
                    .GetMethods()
                    .Where(x => x.Name=="Create" && x.IsPublic && x.IsGenericMethod && !x.HasResultParameter())
                    .Single();
                var createMethod = method.MakeGenericMethod(genericSubType);
                var exampleRequestObjectInstance = createMethod.Invoke(null,new object[] { genericTypeInstance,null,null });
                jsonStrExample = JsonSerializer.Serialize(exampleRequestObjectInstance, typeof(T), SwaggerGenerationOptionsExtension.JsonSerializerOptions);
            }

            Dictionary<string, OpenApiSchema> openApiProps = new Dictionary<string, OpenApiSchema>();
            var props = abstractType.GetProperties()
                                    .Where(x=>x.IsPublic() && x.IsPubliclyWritable() && x.IsPubliclyReadable());
            foreach (var prop in props)
            {
                var openApiSchemaItem = prop?.GetOpenApiSchemaByType();
                if (openApiSchemaItem is null)
                    throw new NotSupportedException();

                openApiProps.Add(openApiSchemaItem.Value.Key, openApiSchemaItem.Value.Schema);
            }

            // Add the reference to the parent schema in the document components
            swaggerDoc.Components.Schemas[abstractSchemaId] = new OpenApiSchema
            {
                Properties = openApiProps,
                Example = jsonStrExample is null?null:new OpenApiString(jsonStrExample),
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.Schema,
                    Id = abstractSchemaId

                }
            };
        }

    }
    public static class OpenApiSchemaExtension
    {
        public static Dictionary<Type, (string type, string format)> NetToSwaggerTypeMapping { get; } = new Dictionary<Type, (string, string)>
        {
            { typeof(string), ("string", null) },
            { typeof(int), ("integer", "int32") },
            { typeof(long), ("integer", "int64") },
            { typeof(float), ("number", "float") },
            { typeof(double), ("number", "double") },
            { typeof(decimal), ("number", "decimal") },
            { typeof(bool), ("boolean", null) },
            { typeof(object), ("object", null) },
            { typeof(void), ("void", null) },
            { typeof(DateTime), ("string", "date-time") },
            { typeof(TimeSpan), ("string", "duration") },
            { typeof(byte[]), ("string", "byte") },
            { typeof(Guid), ("string", "uuid") },
            { typeof(Uri), ("string", "uri") },
        };
        public static (string Type, string Format)? GetSwaggerTypeDefinition(Type type) => NetToSwaggerTypeMapping.TryGetValue(type, out var typeMapping) ? typeMapping : null;

        public static (string Key, OpenApiSchema Schema) GetOpenApiSchemaByType(this PropertyInfo propertyInfo)
        {
            var propAtt = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            string propName = (propAtt?.Name) ?? propertyInfo.Name;

            string key = propName;
            bool propCanBeNull = propertyInfo.IsNullable();
            (string type, string format) swaggerTypeDefinition = GetSwaggerTypeDefinition(propertyInfo.PropertyType) ?? ("string", "string");

            OpenApiSchema openApiSchema = new OpenApiSchema();
            openApiSchema.Type = swaggerTypeDefinition.type;
            openApiSchema.Format = swaggerTypeDefinition.format;
            openApiSchema.Nullable = propCanBeNull;
            openApiSchema.Title = $"NET-Type: {(propertyInfo.PropertyType.IsGenericType?(($"{propertyInfo.PropertyType.Name}<{propertyInfo.PropertyType?.GetGenericArguments()?.First().Name}>")):(propertyInfo.PropertyType.Name))}";
            return (key, openApiSchema);
        }
    }
}
