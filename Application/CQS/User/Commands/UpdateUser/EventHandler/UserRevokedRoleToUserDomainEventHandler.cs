﻿using Domain.Entities.Users.Events;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
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
