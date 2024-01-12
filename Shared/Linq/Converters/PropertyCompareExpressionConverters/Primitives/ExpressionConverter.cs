using System.ComponentModel;
using System.Linq.Expressions;
using static Shared.DataFilter.Infrastructure.ColumnFilterConst;

namespace Shared.Linq.Converters.PropertyCompareExpressionConverters.Primitives
{
    public interface IExpressionFilter
    {
        public Expression Convert(MemberExpression memberExpression, ExpressionFilter filter);
    }
    internal abstract class ExpressionConverter<T> : IExpressionFilter
    {
        protected ConstantExpression ConstantExpression { get; set; }
        protected ExpressionConverter()
        {

        }
        public virtual ConstantExpression ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            if(memberExpression.Type == typeof(T))
            {
                ConstantExpression = Expression.Constant(filter.Value);
                return ConstantExpression;
            }
            var converter = TypeDescriptor.GetConverter(memberExpression.Type);
            var constantValue = converter.ConvertTo(filter.Value, typeof(T));
            var constant = Expression.Constant(constantValue);
            if (ConstantExpression is null)
            {
                ConstantExpression = constant;
            }
            return ConstantExpression;
        }
        public virtual Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            Expression expression = null;

            var constant = ConvertValueToConstantExpression(memberExpression,filter);
            switch (filter.Operator)
            {
                case OPERATOR.EQUAL:
                    expression = Expression.Equal(memberExpression, constant);
                    break;
                case OPERATOR.NOT_EQUAL:
                    expression = Expression.NotEqual(memberExpression, constant);
                    break;
                default:
                    break;
            }
            return expression;
        }
    }
}
