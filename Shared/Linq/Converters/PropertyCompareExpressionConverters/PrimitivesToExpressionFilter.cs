using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq.Converters.PropertyCompareExpressionConverters
{
    /// <summary>
    /// For Types like string, int, double, long etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class PrimitivesToExpressionFilter<T> : ExpressionConverter<T>
        where T : struct
    {
        public override Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            var expression = base.Convert(memberExpression, filter);
            if (expression is null && (memberExpression is int || memberExpression is double || memberExpression is long))
            {
                switch (filter.Operator)
                {
                    case OPERATOR.LESS_THAN:
                        expression = Expression.LessThan(memberExpression, ConstantExpressions.First());
                        break;
                    case OPERATOR.LESS_THAN_OR_EQUAL:
                        expression = Expression.LessThanOrEqual(memberExpression, ConstantExpressions.First());
                        break;
                    case OPERATOR.GREATER_THAN:
                        expression = Expression.GreaterThan(memberExpression, ConstantExpressions.First());
                        break;
                    case OPERATOR.GREATER_THAN_OR_EQUAL:
                        expression = Expression.GreaterThanOrEqual(memberExpression, ConstantExpressions.First());
                        break;
                    case OPERATOR.BETWEEN:
                        var left = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(memberExpression, ConstantExpressions.First()));
                        var right = Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(memberExpression, ConstantExpressions.Skip(1).First()));

                        expression = Expression.And(left, right);
                        break;
                    default:
                        break;
                }
            }
            return expression;
        }

    }
}
