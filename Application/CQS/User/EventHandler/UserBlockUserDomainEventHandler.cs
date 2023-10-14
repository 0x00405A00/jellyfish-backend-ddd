using MediatR;

namespace Domain.Entities.User.Event
{
    internal sealed class UserBlockUserDomainEventHandler :
        INotificationHandler<UserBlockUserDomainEvent>
    {
        public UserBlockUserDomainEventHandler()
        {

        }

        public Task Handle(UserBlockUserDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
