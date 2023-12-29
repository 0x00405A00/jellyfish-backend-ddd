using Domain.Entities.Users.Events;
using Domain.Primitives.Ids;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
{
    internal sealed class UserUpdatedDomainEventHandler :
        INotificationHandler<UserUpdatedDomainEvent<Domain.Entities.Users.User, UserId>>
    {
        public UserUpdatedDomainEventHandler()
        {

        }

        public async Task Handle(UserUpdatedDomainEvent<Domain.Entities.Users.User, UserId> notification, CancellationToken cancellationToken)
        {

        }
    }
}
