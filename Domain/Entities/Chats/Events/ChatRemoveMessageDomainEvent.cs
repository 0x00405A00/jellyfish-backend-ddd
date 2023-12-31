﻿using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Chats.Events
{
    public record ChatRemoveMessageDomainEvent(ChatId ChatId, UserId ActorUserId, UserId MessageOwnerUserId, MessageId MessageId) : DomainEvent(ChatId)
    {
    }
}
