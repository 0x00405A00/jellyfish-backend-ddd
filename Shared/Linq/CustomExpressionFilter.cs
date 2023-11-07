using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Shared.Linq
{
    public static partial class CustomExpressionFilter<T> where T : class
    {
        public static Expression<Func<T, bool>> CustomFilter(List<ColumnFilter> columnFilters, string className)
        {
            Expression<Func<T, bool>> filters = null;
            bool isAnyInvalidOperatorIncluded = columnFilters.Any(x => x.IsInvalidOperator);
            if(isAnyInvalidOperatorIncluded)
            {
                throw new InvalidOperationException($"{columnFilters} include some invalid filters (Op==OPERATOR.INVALID)");
            }
            try
            {
                var expressionFilters = new List<ExpressionFilter>();
                foreach (var item in columnFilters)
                {
                    expressionFilters.Add(new ExpressionFilter() { ColumnName = item.field, Value = item.value, Operator = item.Op });
                }
                // Create the parameter expression for the input data
                var parameter = Expression.Parameter(typeof(T), className);

                // Build the filter expression dynamically
                Expression filterExpression = null;
                foreach (var filter in expressionFilters)
                {
                    var property = Expression.Property(parameter, filter.ColumnName);

                    Expression comparison;

                    if (property.Type == typeof(string))
                    {
                        var constant = Expression.Constant(filter.Value);
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                    }
                    else if (property.Type == typeof(double))
                    {
                        var constant = Expression.Constant(Convert.ToDouble(filter.Value));
                        comparison = Expression.Equal(property, constant);
                    }
                    else if (property.Type == typeof(Guid))
                    {
                        var constant = Expression.Constant(Guid.Parse(filter.Value));
                        comparison = Expression.Equal(property, constant);
                    }
                    else
                    {
                        var constant = Expression.Constant(Convert.ToInt32(filter.Value));
                        comparison = Expression.Equal(property, constant);
                    }


                    filterExpression = filterExpression == null
                        ? comparison
                        : Expression.And(filterExpression, comparison);
                }

                // Create the lambda expression with the parameter and the filter expression
                filters = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
            }
            catch (Exception)
            {
                filters = null;
            }
            return filters;
        }
    }
}
