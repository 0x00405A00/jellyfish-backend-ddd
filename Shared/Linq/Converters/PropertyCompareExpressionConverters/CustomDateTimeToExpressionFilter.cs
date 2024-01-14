using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives;
using System.Linq.Expressions;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq.Converters.PropertyCompareExpressionConverters
{
    internal class CustomDateTimeToExpressionFilter<T> : ExpressionConverter<T>
        where T : CustomDateTime
    {
        public override Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            Expression expression = null;
            ConvertValueToConstantExpression(memberExpression, filter);

            switch (filter.Operator)
            {
                case OPERATOR.EQUAL:
                    expression = Expression.Equal(memberExpression, ConstantExpressions.First());
                    break;
                case OPERATOR.CONTAINS:

                    var dbFunctionStaticInstance = Expression.Constant(EF.Functions);
                    var propertyCallImplicitConversation = Expression.Convert(memberExpression, typeof(string));
                    var concatMethod = typeof(string).GetMethod(nameof(string.Concat), new[] { typeof(string), typeof(string) });
                    var filterValueExpression = Expression.Add(
                                                        Expression.Add(
                                                            Expression.Constant("%"),
                                                            Expression.Constant(filter.Values.First()),
                                                            concatMethod),
                                                        Expression.Constant("%"),
                                                        concatMethod);
                    var dbFunctionLikeMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) });
                    expression = Expression.Call(
                        dbFunctionLikeMethod,
                        dbFunctionStaticInstance,
                        propertyCallImplicitConversation,
                        filterValueExpression);
                    break;
                case OPERATOR.BETWEEN:
                    var left = Expression.GreaterThanOrEqual(memberExpression, ConstantExpressions.First());
                    var right = Expression.LessThanOrEqual(memberExpression, ConstantExpressions.Skip(1).First());

                    expression = Expression.And(left, right);
                    break;
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
                case OPERATOR.NOT_EQUAL:
                    expression = Expression.NotEqual(memberExpression, ConstantExpressions.First());
                    break;
                default:
                    break;
            }
            return expression;
        }
        public override ConstantExpression[] ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            List<ConstantExpression> constantExpressions = new();
            foreach(var val in filter.Values)
            {

                object constantValue = null;
                ConstantExpression constantExpression = null;
                if (DateTime.TryParse(val, out DateTime result))
                {
                    constantValue = result;
                    constantExpression = Expression.Constant(new CustomDateTime(result), typeof(CustomDateTime));
                }
                else if (filter.Values is not null)
                {
                    constantValue = val;
                    constantExpression = Expression.Constant(constantValue);
                }
                else
                {
                    constantValue = val;
                    constantExpression = Expression.Constant(constantValue);
                }
                constantExpressions.Add(constantExpression);
            }
            ConstantExpressions = constantExpressions.OrderBy(x=>x.Value).ToArray();
            return ConstantExpressions;
        }
    }
}
