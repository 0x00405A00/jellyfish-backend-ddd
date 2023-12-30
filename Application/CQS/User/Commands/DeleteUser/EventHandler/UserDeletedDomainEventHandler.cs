using Domain.Entities.Users.Events;
using MediatR;

namespace Application.CQS.User.Commands.DeleteUser.EventHandler
{
    internal sealed class UserDeletedDomainEventHandler :
        INotificationHandler<UserDeletedDomainEvent>
    {
        public UserDeletedDomainEventHandler()
        {

        }

        public Task Handle(UserDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
