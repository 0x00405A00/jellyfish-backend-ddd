using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.User.EventHandler
{
    internal sealed class UserCreatedDomainEventHandler :
        INotificationHandler<UserCreatedDomainEvent>
    {
        public UserCreatedDomainEventHandler()
        {

        }

        public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
