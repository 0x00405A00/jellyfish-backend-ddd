using Shared.DataFilter.Infrastructure;
using System;
using System.Linq.Expressions;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq
{
    public static partial class CustomExpressionFilter<T> where T : class
    {
        public static Expression<Func<T, bool>> CustomFilter(List<ColumnFilterGroup> columnFilterGroup, string className)
        {
            Expression filterExpression = null;
            Expression<Func<T, bool>> filters = null;
            var parameter = Expression.Parameter(typeof(T), className);
            foreach (ColumnFilterGroup group in columnFilterGroup)
            {
                var columnFilters = group.Filters;
                bool isAnyInvalidOperatorIncluded = columnFilters.Any(x => x.IsInvalidOperator);
                if (isAnyInvalidOperatorIncluded)
                {
                    throw new InvalidOperationException($"{group.Group} with {group.Filters.Count} conditions include some invalid filters (Op==OPERATOR.INVALID)");
                }
                try
                {
                    var expressionFilters = new List<ExpressionFilter>();
                    foreach (var item in columnFilters)
                    {
                        expressionFilters.Add(new ExpressionFilter()
                        {
                            ColumnName = item.field,
                            Value = item.value,
                            Operator = item.Op
                        });
                    }
                    // Create the parameter expression for the input data

                    // Build the filter expression dynamically
                    foreach (var filter in expressionFilters)
                    {
                        var property = Expression.Property(parameter, filter.ColumnName);

                        Expression comparison = GetExpressionByOperator(property, filter);


                        filterExpression = filterExpression == null
                            ? comparison
                            : (GetConditionGroup(filterExpression,comparison,group));

                    }

                    // Create the lambda expression with the parameter and the filter expression
                    filters = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);

                }
                catch (Exception)
                {
                    filters = null;
                }
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
            Expression comparison = null;
            if (property.Type == typeof(string))
            {
                var constant = Expression.Constant(filter.Value);
                switch(filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.CONTAINS:
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            else if (property.Type == typeof(double)|| property.Type == typeof(double?))
            {
                var constant = Expression.Constant(Convert.ToDouble(filter.Value));
                switch (filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.LESS_THAN:
                        comparison = Expression.LessThan(property, constant);
                        break;
                    case OPERATOR.LESS_THAN_OR_EQUAL:
                        comparison = Expression.LessThanOrEqual(property, constant);
                        break;
                    case OPERATOR.GREATER_THAN:
                        comparison = Expression.GreaterThan(property, constant);
                        break;
                    case OPERATOR.GREATER_THAN_OR_EQUAL:
                        comparison = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            else if (property.Type == typeof(Guid))
            {
                var constant = Expression.Constant(Guid.Parse(filter.Value));
                switch (filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.CONTAINS:
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            else if (property.Type == typeof(DateTime)|| property.Type == typeof(DateTime?))
            {
                DateTime? value = filter.Value == null ? null : DateTime.Parse(filter.Value);
                var constant = Expression.Constant(value);
                switch (filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.CONTAINS:
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            else if (property.Type == typeof(DateOnly)||property.Type == typeof(DateOnly?))
            {
                var constant = Expression.Constant(DateOnly.Parse(filter.Value));
                switch (filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.CONTAINS:
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            else if (property.Type == typeof(bool) || property.Type == typeof(bool?))
            {
                var constant = Expression.Constant(bool.Parse(filter.Value));
                switch (filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            else if(property.Type == typeof(int) || property.Type == typeof(int?)) 
            {
                var constant = Expression.Constant(Convert.ToInt32(filter.Value));
                switch (filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.LESS_THAN:
                        comparison = Expression.LessThan(property, constant);
                        break;
                    case OPERATOR.LESS_THAN_OR_EQUAL:
                        comparison = Expression.LessThanOrEqual(property, constant);
                        break;
                    case OPERATOR.GREATER_THAN:
                        comparison = Expression.GreaterThan(property, constant);
                        break;
                    case OPERATOR.GREATER_THAN_OR_EQUAL:
                        comparison = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            else
            {
                var constant = Expression.Constant(filter.Value);
                switch (filter.Operator)
                {
                    case OPERATOR.EQUAL:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case OPERATOR.NOT_EQUAL:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }
            return comparison;
        }
    }
}
