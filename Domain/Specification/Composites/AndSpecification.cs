using System.Linq.Expressions;

namespace Domain.Specification.Composites;

public class AndSpecification<T> : Specification<T>
{
    public AndSpecification(params Specification<T>[] specifications)
    {
        _specifications = specifications.ToList();
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        if (_specifications.Count == 0)
        {
            return entity => true;
        }
        Expression<Func<T, bool>> combinedExpression = _specifications[0].ToExpression();

        for (int i = 1; i < _specifications.Count; i++)
        {
            Expression<Func<T, bool>> nextExpression = _specifications[i].ToExpression();
            combinedExpression = CombineExpressions(combinedExpression, nextExpression, Expression.AndAlso);
        }

        return combinedExpression;
    }

}