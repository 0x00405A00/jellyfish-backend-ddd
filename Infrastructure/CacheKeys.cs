using Infrastructure.Abstractions;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public static class CacheKeys
    {
        public static Func<Guid, string> UserKey = (g) => $"user-{g.ToString()}";
        public static Func<string, string> UsersKey = (g) => $"user-{g}";

        public static Guid? ExtractUuid<T>(this Expression<Func<T, bool>> expression)
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
                    // Der rechte Ausdruck ist ein MemberAccess für die Uuid-Eigenschaft.
                    // Extrahieren Sie den Wert aus dem rechten MemberAccess.
                    var t = rightMemberExpression.Expression.GetType();
                    var tt = ((MemberExpression)(rightMemberExpression).Expression).Expression;
                    var ttt = tt.GetType();
                    if (rightMemberExpression.Member.Name == "Id" && ttt ==typeof(ConstantExpression))
                    {
                        var val = (((ConstantExpression)tt).Value);
                        var tttt = val.GetType();
                        return (Guid)Guid.Empty;
                    }
                }
            }

            return null;
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
