using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives;
using System.Linq.Expressions;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq.Converters.PropertyCompareExpressionConverters
{
    /// <summary>
    /// Converter for ValueObject like Email, PhoneNumber, Password etc.
    /// </summary>
    /// <typeparam name="T">A type that implments IValueObjectNonBinary</typeparam>
    internal class ValueObjectNonBinaryToExpressionFilter<T> : ExpressionConverter<T>
        where T : IValueObjectNonBinary
    {
        public override Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            Expression expression = null;
            ConvertValueToConstantExpression(memberExpression, filter);

            var propertyCallImplicitConversation = Expression.Convert(memberExpression, typeof(string));
            switch (filter.Operator)
            {
                case OPERATOR.EQUAL:
                    expression = Expression.Equal(propertyCallImplicitConversation, ConstantExpressions.First());
                    break;
                case OPERATOR.CONTAINS:

                    var concatMethod = typeof(string).GetMethod(nameof(string.Concat), new[] { typeof(string), typeof(string) });
                    var filterValueExpression = Expression.Add(
                                                        Expression.Add(
                                                            Expression.Constant("%"),
                                                            Expression.Constant(filter.Values.First()),
                                                            concatMethod),
                                                        Expression.Constant("%"),
                                                        concatMethod);
                    var dbFunctionStaticInstance = Expression.Constant(EF.Functions);
                    var dbFunctionLikeMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) });
                    expression = Expression.Call(
                        dbFunctionLikeMethod,
                        dbFunctionStaticInstance,
                        propertyCallImplicitConversation,
                        filterValueExpression);
                    break;
                case OPERATOR.NOT_EQUAL:
                    expression = Expression.NotEqual(propertyCallImplicitConversation, ConstantExpressions.First());
                    break;
                default:
                    break;
            }
            return expression;
        }
        public override ConstantExpression[] ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            List<ConstantExpression> result = new List<ConstantExpression>();
            foreach (var val in filter.Values)
            {
                var constant = Expression.Constant(val);
                result.Add(constant);
            }
            ConstantExpressions = result.ToArray();
            return ConstantExpressions;
        }
    }
}
