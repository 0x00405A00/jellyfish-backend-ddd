using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.User.Commands.Roles.EventHandler
{
    internal sealed class UserRevokedRoleToUserDomainEventHandler :
        INotificationHandler<UserRevokedRoleToUserDomainEvent>
    {
        public UserRevokedRoleToUserDomainEventHandler()
        {

        }

        public Task Handle(UserRevokedRoleToUserDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
