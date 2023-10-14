﻿using Domain.Entities.Chats.Event;
using Domain.Primitives;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatUpdatedDomainEventHandler :
        INotificationHandler<ChatUpdatedDomainEvent>
    {
        public ChatUpdatedDomainEventHandler()
        {

        }

        public Task Handle(ChatUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
