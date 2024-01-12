using System.Linq.Expressions;
using Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq.Converters.PropertyCompareExpressionConverters
{
    internal class StringToExpressionFilter<T> : ExpressionConverter<T>
        where T : notnull
    {
        public override Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            var expression = base.Convert(memberExpression, filter);

            if (expression is null)
            {
                switch (filter.Operator)
                {
                    case OPERATOR.CONTAINS:
                        expression = Expression.Call(memberExpression, nameof(string.Contains), Type.EmptyTypes, ConstantExpression);
                        break;
                    default:
                        break;
                }
            }
            return expression;
        }
    }
}
