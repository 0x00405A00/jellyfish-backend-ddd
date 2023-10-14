using System.Linq.Expressions;

namespace Domain.Primitives
{
    public record UpdatedDomainEvent<TEntity, TEntityId>(TEntity e, Expression<Func<TEntity, object>> expresion, object value) : DomainEvent(e) 
        where TEntity : Entity<TEntityId>
        where TEntityId : Identification
    {
    }
}
