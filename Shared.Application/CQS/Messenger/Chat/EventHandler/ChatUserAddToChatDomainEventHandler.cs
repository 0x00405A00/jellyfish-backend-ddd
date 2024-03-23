using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatUserAddToChatDomainEventHandler :
        INotificationHandler<ChatUserAddToChatDomainEvent>
    {
        public ChatUserAddToChatDomainEventHandler()
        {

        }

        public Task Handle(ChatUserAddToChatDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
