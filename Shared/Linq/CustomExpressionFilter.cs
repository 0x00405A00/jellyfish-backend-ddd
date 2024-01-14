using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;
using Shared.Linq.Converters;
using System.Linq.Expressions;
using System.Reflection;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq
{
    [Keyless]
    public static class CustomDbFunctions
    {
        /*[DbFunction("emailcontains", IsBuiltIn = true, IsNullable = true)]
        public static bool EmailContains(string emailValue, string value)=> throw new InvalidOperationException(); */
        /*[DbFunction("TestCalc", IsBuiltIn = true, IsNullable = true)]
        public static int Test(int t1, int t2) => throw new InvalidOperationException();

        [DbFunction("PhoneNumberContains", IsBuiltIn = true, IsNullable = true)]
        public static bool PhoneNumberContains(string phoneNumber, string value) => throw new InvalidOperationException();*/

    }
    public static class InstanceHelper
    {
        public static T Construct<T>(Type[] paramTypes, object[] paramValues)
        {
            Type t = typeof(T);

            ConstructorInfo ctorInfo = t.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, paramTypes, null);

            return (T)ctorInfo.Invoke(paramValues);
        }
        public static object GetValue(MemberExpression member)
        {
            return Expression.Lambda(member).Compile().DynamicInvoke();
        }
        public static Expression<Func<T, bool>> Like<T>(Expression<Func<T, string>> prop, string keyword)
        {
            var concatMethod = typeof(string).GetMethod(nameof(string.Concat), new[] { typeof(string), typeof(string) });
            return Expression.Lambda<Func<T, bool>>(
                Expression.Call(
                    typeof(DbFunctionsExtensions),
                    nameof(DbFunctionsExtensions.Like),
                    null,
                    Expression.Constant(EF.Functions),
                    prop.Body,
                    Expression.Add(
                        Expression.Add(
                            Expression.Constant("%"),
                            Expression.Constant(keyword),
                            concatMethod),
                        Expression.Constant("%"),
                        concatMethod)),
                prop.Parameters);
        }
    }
    public static partial class CustomExpressionFilter<T> where T : class
    {

        public static Expression<Func<T, bool>> CustomFilter(List<ColumnFilterGroup> columnFilterGroup, string className)
        {
            Expression filterExpressionFinal = null;
            Expression<Func<T, bool>> filters = null;
            var parameter = Expression.Parameter(typeof(T), className);
            var properties  = typeof(T).GetProperties();
            try
            {
                foreach (ColumnFilterGroup group in columnFilterGroup)
                {
                    var columnFilters = group.Filters;
                    bool isAnyInvalidOperatorIncluded = columnFilters.Any(x => x.IsInvalidOperator);
                    if (isAnyInvalidOperatorIncluded)
                    {
                        throw new InvalidOperationException($"{group.Group} with {group.Filters.Count} conditions include some invalid filters (Op==OPERATOR.INVALID)");
                    }
                    var expressionFilters = new List<ExpressionFilter>();
                    foreach (var item in columnFilters)
                    {
                        if (!properties.Any(x => x.Name == item.field))
                        {
                            continue;
                        }
                        expressionFilters.Add(new ExpressionFilter()
                        {
                            ColumnName = item.field,
                            Values = item.GetValueCollection(),
                            Operator = item.Op
                        });
                    }
                    // Create the parameter expression for the input data

                    // Build the filter expression dynamically
                    Expression filterExpression = null;
                    foreach (var filter in expressionFilters)
                    {
                        var property = Expression.Property(parameter, filter.ColumnName);

                        Expression comparison = GetExpressionByOperator(property, filter);

                        comparison = (comparison);// ?? (Expression.Equal(Expression.Constant(true), Expression.Constant(true))));
                        if(comparison != null)
                        {
                            filterExpression = filterExpression == null
                                ? comparison
                                : (GetConditionGroup(filterExpression, comparison, group));
                        }

                        
                    }
                    filterExpressionFinal = filterExpressionFinal == null ? filterExpression : Expression.AndAlso(filterExpressionFinal, filterExpression);


                }
                if(filterExpressionFinal is not null)
                    filters = Expression.Lambda<Func<T, bool>>(filterExpressionFinal, parameter);
            }
            catch (Exception ex)
            {
                throw;
            }
            return filters;
        }

        public static Expression GetConditionGroup(Expression filterExpression, Expression comparison, ColumnFilterGroup filterGroup)
        {
            Expression conditionGroup = null;
            switch(filterGroup.OpGroup)
            {
                case OPERATOR_GROUP.AND:
                    conditionGroup = Expression.AndAlso(filterExpression, comparison);//Mysql AND
                    break;
                case OPERATOR_GROUP.XOR:
                    conditionGroup = CreateXorCondition(filterExpression, comparison);//Mysql XOR
                    break;
                case OPERATOR_GROUP.OR:
                    conditionGroup = CreateOrCondition(filterExpression, comparison);//Mysql ||
                    break;
                default:
                    conditionGroup = Expression.AndAlso(filterExpression, comparison);
                    break;
            }
            return conditionGroup;
        }
        /// <summary>
        /// Create Linq Expression for OR Condition
        /// </summary>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public static Expression CreateOrCondition(Expression exp1, Expression exp2)
        {
            return Expression.OrElse(exp1, exp2);
        }
        /// <summary>
        /// Create Linq Expression for XOR Condition
        /// </summary>
        /// <param name="condition1"></param>
        /// <param name="condition2"></param>
        /// <returns></returns>
        public static Expression CreateXorCondition(Expression condition1, Expression condition2)
        {
            BinaryExpression andExpression = Expression.AndAlso(condition1, condition2);
            UnaryExpression notExpression = Expression.Not(condition2);
            BinaryExpression xorExpression = Expression.AndAlso(andExpression, notExpression);

            return xorExpression;
        }

        public static Expression GetExpressionByOperator(MemberExpression property, ExpressionFilter filter)
        {
            var converter = ExpressionFilterExtension.GetExpressionByTypeConverter(property.Type);
            if (converter is null)
                return null;
            Expression comparison = converter.Convert(property,filter);
            return comparison;
        }
       
    }
}
