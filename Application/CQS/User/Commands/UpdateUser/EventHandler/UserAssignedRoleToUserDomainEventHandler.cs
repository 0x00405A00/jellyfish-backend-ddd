using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
{
    internal sealed class UserAssignedRoleToUserDomainEventHandler :
        INotificationHandler<UserAssignedRoleToUserDomainEvent>
    {
        public UserAssignedRoleToUserDomainEventHandler()
        {

        }

        public Task Handle(UserAssignedRoleToUserDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
