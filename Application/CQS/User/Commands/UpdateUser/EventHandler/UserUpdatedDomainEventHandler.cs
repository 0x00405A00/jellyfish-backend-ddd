using Domain.Entities.User;
using Domain.Entities.User.Event;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
{
    internal sealed class UserUpdatedDomainEventHandler :
        INotificationHandler<UserUpdatedDomainEvent<Domain.Entities.User.User, UserId>>
    {
        public UserUpdatedDomainEventHandler()
        {

        }

        public async Task Handle(UserUpdatedDomainEvent<Domain.Entities.User.User, UserId> notification, CancellationToken cancellationToken)
        {

        }
    }
}
