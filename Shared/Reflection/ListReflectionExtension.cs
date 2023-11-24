namespace Shared.Reflection
{
    public static class ListReflectionExtension
    {
        public static bool IsListAndGenericTypeImplementsT<T>(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                Type genericArgumentType = type.GetGenericArguments()[0];

                return typeof(T).IsAssignableFrom(genericArgumentType);
            }
            return false;
        }
    }
}
