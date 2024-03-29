﻿using Domain.Entities.Messages.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class MessageAcknowlegedByUserDomainEventHandler :
        INotificationHandler<MessageAcknowlegedByUserDomainEvent>
    {
        public MessageAcknowlegedByUserDomainEventHandler()
        {

        }

        public Task Handle(MessageAcknowlegedByUserDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
