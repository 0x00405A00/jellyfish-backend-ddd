using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.EventHandler
{
    internal sealed class UserAcceptFriendshipRequestDomainEventHandler :
        INotificationHandler<UserAcceptFriendshipRequestDomainEvent>
    {
        public UserAcceptFriendshipRequestDomainEventHandler()
        {

        }

        public Task Handle(UserAcceptFriendshipRequestDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
