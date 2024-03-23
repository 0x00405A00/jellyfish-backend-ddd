using Domain.Entities.Users.Events;
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
