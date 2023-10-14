using MediatR;

namespace Domain.Entities.User.Event
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
