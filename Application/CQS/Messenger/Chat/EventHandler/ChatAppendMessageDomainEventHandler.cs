using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatAppendMessageDomainEventHandler :
        INotificationHandler<ChatAppendMessageDomainEvent>
    {
        public ChatAppendMessageDomainEventHandler()
        {

        }

        public Task Handle(ChatAppendMessageDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
