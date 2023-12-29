using Domain.Entities.Users.Events;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
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
