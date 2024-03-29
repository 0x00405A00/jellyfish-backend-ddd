﻿using Domain.Entities.Users.Events;
using MediatR;

namespace Application.CQS.User.Commands.UpdateUser.EventHandler
{
    internal sealed class UserRemoveFriendDomainEventHandler :
        INotificationHandler<UserRemoveFriendDomainEvent>
    {
        public UserRemoveFriendDomainEventHandler()
        {

        }

        public Task Handle(UserRemoveFriendDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
