using Domain.ValueObjects;
using System.Reflection;

namespace Shared.Reflection
{
    public static class ReflectionExtension
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

        public static bool IsResultType(Type parameterType)
        {
            return parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Result<>);
        }
        public static bool HasResultParameter(this MethodInfo method)
        {
            return method.GetParameters().Any(parameter => IsResultType(parameter.ParameterType));
        }
        public static bool IsNullable(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsValueType)
            {
                return Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;
            }
            return true;
        }
        /// <summary>
        /// Credits: https://stackoverflow.com/users/22656/jon-skeet
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetNameWithoutGenericArity(this Type t)
        {
            string name = t.Name;
            int index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }
        public static string GenericArgumentsConcatToString(this Type t)
        {
            string str = null;
            var types = t.GenericTypeArguments;
            if (t.IsGenericType && types?.Length > 0)
            {
                for(int i=0;i< types.Length;i++)
                {
                    str += types[i].Name;
                    if (i<= types.Length-2)//Penultimate Item (Vorletztes Item)
                    {
                        str += ",";
                    }
                }
            }
            return str;
        }
    }
}
