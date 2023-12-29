using Domain.Entities.Users.Events;
using MediatR;

namespace Domain.Entities.Users.Events
{
    internal sealed class UserDeletedDomainEventHandler :
        INotificationHandler<UserDeletedDomainEvent>
    {
        public UserDeletedDomainEventHandler()
        {

        }

        public Task Handle(UserDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
