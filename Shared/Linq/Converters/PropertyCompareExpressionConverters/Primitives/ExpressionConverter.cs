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
        protected ConstantExpression[] ConstantExpressions { get; set; }
        protected ExpressionConverter()
        {

        }
        public virtual ConstantExpression[] ConvertValueToConstantExpression(MemberExpression memberExpression, ExpressionFilter filter)
        {
            List<ConstantExpression> constantExpressions = new List<ConstantExpression>();
            foreach(var val in filter.Values)
            {

                if (memberExpression.Type == typeof(T))
                {
                    var exp = Expression.Constant(val);
                    constantExpressions.Add(exp);
                    continue;
                }
                var converter = TypeDescriptor.GetConverter(memberExpression.Type);
                var constantValue = converter.ConvertTo(val, typeof(T));
                var constant = Expression.Constant(constantValue);
                constantExpressions.Add(constant);  
            }
            ConstantExpressions = constantExpressions.ToArray();
            return ConstantExpressions;
        }
        public virtual Expression Convert(MemberExpression memberExpression, ExpressionFilter filter)
        {
            Expression expression = null;

            var constant = ConvertValueToConstantExpression(memberExpression,filter);
            switch (filter.Operator)
            {
                case OPERATOR.EQUAL:
                    expression = Expression.Equal(memberExpression, constant.First());
                    break;
                case OPERATOR.NOT_EQUAL:
                    expression = Expression.NotEqual(memberExpression, constant.First());
                    break;
                default:
                    break;
            }
            return expression;
        }
    }
}
