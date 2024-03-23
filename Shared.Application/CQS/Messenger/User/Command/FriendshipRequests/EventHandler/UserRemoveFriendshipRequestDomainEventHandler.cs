using Domain.Entities.Users.Events;
using MediatR;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.EventHandler
{
    internal sealed class UserRemoveFriendshipRequestDomainEventHandler :
        INotificationHandler<UserRemoveFriendshipRequestDomainEvent>
    {
        public UserRemoveFriendshipRequestDomainEventHandler()
        {

        }

        public Task Handle(UserRemoveFriendshipRequestDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
