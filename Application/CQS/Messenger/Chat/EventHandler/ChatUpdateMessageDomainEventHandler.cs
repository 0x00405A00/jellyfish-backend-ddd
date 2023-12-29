using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatUpdateMessageDomainEventHandler :
        INotificationHandler<ChatUpdateMessageDomainEvent>
    {
        public ChatUpdateMessageDomainEventHandler()
        {

        }

        public Task Handle(ChatUpdateMessageDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
