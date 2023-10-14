using Domain.Entities.Chats.Event;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.UpdateChat.EventHandler
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
