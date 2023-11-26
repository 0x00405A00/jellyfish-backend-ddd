using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.User.Commands.Roles.EventHandler
{
    internal sealed class UserAssignedRoleToUserDomainEventHandler :
        INotificationHandler<UserAssignedRoleToUserDomainEvent>
    {
        public UserAssignedRoleToUserDomainEventHandler()
        {

        }

        public async Task Handle(UserAssignedRoleToUserDomainEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}
