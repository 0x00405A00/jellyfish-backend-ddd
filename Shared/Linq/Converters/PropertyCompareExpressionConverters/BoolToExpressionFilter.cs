using System.Linq.Expressions;
using Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives;

namespace Shared.Linq.Converters.PropertyCompareExpressionConverters
{
    internal class BoolToExpressionFilter<T> : ExpressionConverter<T>
        where T : struct
    {
        public override Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            var expression = base.Convert(memberExpression, filter);

            return expression;
        }
        public override ConstantExpression ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            var constant = Expression.Constant(bool.Parse(filter.Value));
            return constant;
        }
    }
}
