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
                        expression = Expression.Call(memberExpression, "Contains", Type.EmptyTypes, ConstantExpressions);
                        break;
                    default:
                        break;
                }
            }
            return expression;
        }
        public override ConstantExpression[] ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            List<ConstantExpression> constantExpressions = new();
            foreach (var val in filter.Values)
            {

                DateTime? value = filter.Values == null ? null : DateTime.Parse(val);
                var constant = Expression.Constant(value);
                constantExpressions.Add(constant);
            }
            ConstantExpressions = constantExpressions.ToArray();
            return ConstantExpressions;
        }
    }
}
