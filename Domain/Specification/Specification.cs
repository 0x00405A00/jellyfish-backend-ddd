using Domain.Specification.Composites;
using System.Linq.Expressions;

namespace Domain.Specification;

public abstract class Specification<T> : ISpecification<T>
{
    protected List<Specification<T>> _specifications = new();
    public abstract Expression<Func<T, bool>> ToExpression();

    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }
    protected Expression<Func<T, bool>> CombineExpressions(
        Expression<Func<T, bool>> left, Expression<Func<T, bool>> right, Func<Expression, Expression, BinaryExpression> combineFunc)
    {

        return CombineExpressions(left,right,combineFunc);
    }

    protected Expression<Func<T, bool>> CombineExpressions<T>(
        Expression<Func<T, bool>> left, Expression<Func<T, bool>> right, Func<Expression, Expression, BinaryExpression> combineFunc)
    {
        BinaryExpression combinedExpression = combineFunc(left.Body, right.Body);
        return Expression.Lambda<Func<T, bool>>(combinedExpression, left.Parameters.Single());
    }
    public void AddSpecification(Specification<T> specification)
    {
        _specifications.Add(specification);
    }
}


