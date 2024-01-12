using System.Linq.Expressions;
using Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq.Converters.PropertyCompareExpressionConverters
{
    internal class DateTimeToExpressionFilter<T> : ExpressionConverter<T>
        where T : struct
    {
        public override Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            var expression = base.Convert(memberExpression, filter);

            if (expression is null)
            {
                switch (filter.Operator)
                {
                    case OPERATOR.CONTAINS:
                        expression = Expression.Call(memberExpression, "Contains", Type.EmptyTypes, ConstantExpression);
                        break;
                    default:
                        break;
                }
            }
            return expression;
        }
        public override ConstantExpression ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            DateTime? value = filter.Value == null ? null : DateTime.Parse(filter.Value);
            var constant = Expression.Constant(value);
            return constant;
        }
    }
}
