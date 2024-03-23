using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatDeletedDomainEventHandler :
        INotificationHandler<ChatDeletedDomainEvent>
    {
        public ChatDeletedDomainEventHandler()
        {

        }

        public Task Handle(ChatDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
