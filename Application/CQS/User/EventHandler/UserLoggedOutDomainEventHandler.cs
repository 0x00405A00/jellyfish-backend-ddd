using MediatR;

namespace Domain.Entities.User.Event
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
