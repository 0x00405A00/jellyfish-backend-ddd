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
            var concatMethod = typeof(string).GetMethod(nameof(string.Concat), new[] { typeof(string), typeof(string) });
            var filterValueExpression = Expression.Add(
                                                Expression.Add(
                                                    Expression.Constant("%"),
                                                    Expression.Constant(filter.Value),
                                                    concatMethod),
                                                Expression.Constant("%"),
                                                concatMethod);
            var dbFunctionStaticInstance = Expression.Constant(EF.Functions);

            switch (filter.Operator)
            {
                case OPERATOR.EQUAL:
                    expression = Expression.Equal(propertyCallImplicitConversation, ConstantExpression);
                    break;
                case OPERATOR.CONTAINS:

                    var dbFunctionLikeMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) });
                    expression = Expression.Call(
                        dbFunctionLikeMethod,
                        dbFunctionStaticInstance,
                        propertyCallImplicitConversation,
                        filterValueExpression);
                    break;
                case OPERATOR.NOT_EQUAL:
                    expression = Expression.NotEqual(propertyCallImplicitConversation, ConstantExpression);
                    break;
                default:
                    break;
            }
            return expression;
        }
        public override ConstantExpression ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            var constant = Expression.Constant(filter.Value);
            if(ConstantExpression is null)
            {
                ConstantExpression = constant;
            }
            return constant;
        }
    }
}
