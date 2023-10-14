using Domain.Primitives;
using System.Linq.Expressions;

namespace Domain.Entities.User.Event
{
    public record UserUpdatedDomainEvent<TEntity,TEntityId>(TEntity e, Expression<Func<TEntity, object>> property, object value) : UpdatedDomainEvent<TEntity, TEntityId>(e, property,value)
        where TEntity : Entity<TEntityId>
        where TEntityId : Identification
    {
    }
}
