using Domain.Entities.Message.Event;
using Domain.Entities.User;
using Domain.Entities.User.Event;
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
