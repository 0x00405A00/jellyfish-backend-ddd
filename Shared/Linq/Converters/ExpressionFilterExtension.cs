using Domain.Primitives;
using Domain.ValueObjects;
using Shared.Linq.Converters.PropertyCompareExpressionConverters;
using Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives;

namespace Shared.Linq.Converters
{
    public static class ExpressionFilterExtension
    {
        private static HashSet<IExpressionFilter> expressionConverters = new HashSet<IExpressionFilter>();

        public static void RegisterType(IExpressionFilter expressionFilter)
        {
            if (expressionConverters.Contains(expressionFilter))
            {
                throw new InvalidOperationException();
            }
            expressionConverters.Add(expressionFilter);
        }
        public static IExpressionFilter GetExpressionByTypeConverter(Type type)
        {
            var allT = expressionConverters.Select(x => x.GetType()).ToList();
            IExpressionFilter expressionFilter = expressionConverters.Where(x => x.GetType().IsGenericType && x.GetType().GenericTypeArguments.Contains(type))
                .FirstOrDefault();
            if(expressionFilter == null)
            {
                return null;
            }
            return (IExpressionFilter)Activator.CreateInstance(expressionFilter.GetType());
        }
        public static void RegisterWellKnownTypes()
        {
            ExpressionFilterExtension.RegisterType(new PrimitivesToExpressionFilter<int>());
            ExpressionFilterExtension.RegisterType(new PrimitivesToExpressionFilter<double>());
            ExpressionFilterExtension.RegisterType(new PrimitivesToExpressionFilter<long>());
            ExpressionFilterExtension.RegisterType(new StringToExpressionFilter<string>());
            ExpressionFilterExtension.RegisterType(new PrimitivesToExpressionFilter<DateTime>());
            ExpressionFilterExtension.RegisterType(new CustomDateTimeToExpressionFilter<CustomDateTime>());
            ExpressionFilterExtension.RegisterType(new GuidToExpressionFilter<Guid>());
            ExpressionFilterExtension.RegisterType(new ValueObjectNonBinaryToExpressionFilter<Email>());
            ExpressionFilterExtension.RegisterType(new ValueObjectNonBinaryToExpressionFilter<PhoneNumber>());
            ExpressionFilterExtension.RegisterType(new ValueObjectNonBinaryToExpressionFilter<Password>());
        }
    }
}
