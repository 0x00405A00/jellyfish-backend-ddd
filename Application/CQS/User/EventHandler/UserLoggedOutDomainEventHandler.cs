using Domain.Entities.Users.Events;
using MediatR;

namespace Domain.Entities.Users.Events
{
    internal sealed class UserLoggedOutDomainEventHandler :
        INotificationHandler<UserLoggedOutDomainEvent>
    {
        public UserLoggedOutDomainEventHandler()
        {

        }

        public Task Handle(UserLoggedOutDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
