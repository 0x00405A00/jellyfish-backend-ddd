using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.EventHandler
{
    internal sealed class UserCreateFriendshipRequestDomainEventHandler :
        INotificationHandler<UserCreateFriendshipRequestDomainEvent>
    {
        public UserCreateFriendshipRequestDomainEventHandler()
        {

        }

        public Task Handle(UserCreateFriendshipRequestDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
