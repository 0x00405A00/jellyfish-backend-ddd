using Domain.Entities.Users.Events;
using MediatR;

namespace Domain.Entities.Users.Events
{
    internal sealed class UserLoggedInDomainEventHandler :
        INotificationHandler<UserLoggedInDomainEvent>
    {
        public UserLoggedInDomainEventHandler()
        {

        }

        public Task Handle(UserLoggedInDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
