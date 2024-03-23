using Domain.Entities.Users.Events;
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
