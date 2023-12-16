using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure
{
    public static class CacheKeys
    {
        public static Func<Guid, string> UserKey = (g) => $"user-{g.ToString()}";
        public static Func<string, string> UsersKey = (g) => $"user-{g}";

        public static T2? ExtractValueFromExpression<T, T2>(this Expression<Func<T, bool>> expression)
            where T : class
        {
            ParameterExpression parameter = expression.Parameters[0];

            if (expression.Body is BinaryExpression binaryExpression)
            {
                if (binaryExpression.Left is MemberExpression memberExpression &&
                    memberExpression.Member.Name == "Uuid" &&
                    memberExpression.Expression == parameter &&
                    binaryExpression.Right is MemberExpression rightMemberExpression)
                {
                    var parentExpression = ((MemberExpression)rightMemberExpression.Expression).Expression;
                    var parentType = parentExpression.GetType();
                    var memberField = (PropertyInfo)rightMemberExpression.Member;

                    if (parentType == typeof(ConstantExpression))
                    {
                        var constantValue = (((ConstantExpression)parentExpression).Value);
                        var constantValueType = constantValue.GetType();
                        var fields = constantValueType.GetFields();

                        foreach (var field in fields) //1. Hierarchie
                        {
                            var fieldValue = field.GetValue(constantValue);
                            var fieldProperties = fieldValue.GetType().GetProperties();

                            foreach (var fieldProperty in fieldProperties) //2. Hierarchie, da Record Value in Record gekapselt und keine direkte Value
                            {
                                var fieldValueProperty = fieldProperty.GetValue(fieldValue);
                                if (fieldValueProperty is null)
                                {
                                    continue;
                                }
                                var memberFieldValue = memberField.GetValue(fieldValue, null);

                                if (fieldValueProperty is T2 && (memberFieldValue is not null) && memberFieldValue.ToString() == fieldValueProperty.ToString())
                                {
                                    return (T2)fieldValueProperty;
                                }
                            }

                        }
                        return default;
                    }
                }
            }

            return default;
        }

        public class UuidExtractor<T> : ExpressionVisitor
            where T : class
        {
            private Guid? Uuid;

            public UuidExtractor()
            {

            }

            public Guid? ExtractUuid(Expression<Func<T, bool>> expression)
            {
                Visit(expression);

                if (Uuid.HasValue)
                {
                    return Uuid.Value;
                }

                return null;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.Name == "Uuid" && node.Member.DeclaringType == typeof(T))
                {
                    var constantExpression = (ConstantExpression)node.Expression;
                    Uuid = (Guid)constantExpression.Value;
                }

                return base.VisitMember(node);
            }
        }
    }
}
