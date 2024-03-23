using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatRemoveMessageDomainEventHandler :
        INotificationHandler<ChatRemoveMessageDomainEvent>
    {
        public ChatRemoveMessageDomainEventHandler()
        {

        }

        public Task Handle(ChatRemoveMessageDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
