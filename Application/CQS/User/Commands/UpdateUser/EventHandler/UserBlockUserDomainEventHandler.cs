using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
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
