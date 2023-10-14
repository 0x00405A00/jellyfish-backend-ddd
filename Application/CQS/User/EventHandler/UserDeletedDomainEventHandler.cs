using MediatR;

namespace Domain.Entities.User.Event
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
