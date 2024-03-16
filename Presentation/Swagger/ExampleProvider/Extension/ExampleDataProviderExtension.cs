using Presentation.Swagger.ExampleProvider.Abstraction;
using Shared.DataTransferObject.Abstraction;
using System.Reflection;

namespace Presentation.Swagger.ExampleProvider.Extension
{
    public static class ExampleDataProviderExtension
    {
        public static IDataTransferObjectExampleProvier<T>? GetExampleProviderObjects<T>()
            where T : IDataTransferObject
        {
            var assembly = Assembly.GetAssembly(typeof(IDataTransferObjectExampleProvier<>));
            var assemblyTypes = assembly.GetTypes();
            var typeResult = assemblyTypes
                .Where(t => t.IsClass && t.IsPublic && t.BaseType == (typeof(AbstractDTOExampleProvider<T>)));
            if (!typeResult.Any())
                return null;
            var type = typeResult.Single();
            return (IDataTransferObjectExampleProvier<T>)Activator.CreateInstance(type);
        }
    }
}
