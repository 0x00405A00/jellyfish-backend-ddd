using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatUserRemoveFromChatDomainEventHandler :
        INotificationHandler<ChatUserRemoveFromChatDomainEvent>
    {
        public ChatUserRemoveFromChatDomainEventHandler()
        {

        }

        public Task Handle(ChatUserRemoveFromChatDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
