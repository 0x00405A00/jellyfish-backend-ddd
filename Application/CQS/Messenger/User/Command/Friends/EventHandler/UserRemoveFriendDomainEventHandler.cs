using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.Messenger.User.Command.Friends.EventHandler
{
    internal sealed class UserRemoveFriendDomainEventHandler :
        INotificationHandler<UserRemoveFriendDomainEvent>
    {
        public UserRemoveFriendDomainEventHandler()
        {

        }

        public Task Handle(UserRemoveFriendDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
