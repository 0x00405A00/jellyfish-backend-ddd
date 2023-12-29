using Domain.Entities.Users.Events;
using Domain.Primitives;
using MediatR;

namespace Domain.Entities.Users.Events
{
    internal sealed class UserUpdatedDomainEventHandler<TEntity,TEntityId> :
        INotificationHandler<UserUpdatedDomainEvent<TEntity, TEntityId>>
        where TEntity : Entity<TEntityId>
        where TEntityId : Identification
    {
        public UserUpdatedDomainEventHandler()
        {

        }

        public Task Handle(UserUpdatedDomainEvent<TEntity, TEntityId> notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
