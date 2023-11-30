using System.Reflection;

namespace Shared.DataTransferObject.Abstraction
{
    public static class DataTransferObjectExtension
    {
        public static IEnumerable<Type> FindDataTransferObjects()
        {
            var assembly = Assembly.GetAssembly(typeof(IDataTransferObject));

            var assemblyTypes = assembly.GetTypes();
            var dataTransferObjectTypes = assemblyTypes
                .Where(type => type.IsClass && type.IsPublic && typeof(IDataTransferObject).IsAssignableFrom(type));

            return dataTransferObjectTypes;
        }
    }
}
