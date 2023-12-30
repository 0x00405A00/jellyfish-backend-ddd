using Domain.Entities.Users.Events;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
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
