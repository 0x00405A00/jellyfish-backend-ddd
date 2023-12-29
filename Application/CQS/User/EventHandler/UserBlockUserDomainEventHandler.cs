using Domain.Entities.Users.Events;
using MediatR;

namespace Domain.Entities.Users.Events
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
