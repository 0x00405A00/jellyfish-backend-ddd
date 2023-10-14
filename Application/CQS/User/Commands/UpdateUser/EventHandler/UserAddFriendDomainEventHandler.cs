using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
{
    internal sealed class UserAddFriendDomainEventHandler :
        INotificationHandler<UserAddFriendDomainEvent>
    {
        public UserAddFriendDomainEventHandler()
        {

        }

        public Task Handle(UserAddFriendDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
