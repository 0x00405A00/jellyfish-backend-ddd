using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.CreateChat.EventHandler
{
    internal sealed class ChatCreatedDomainEventHandler :
        INotificationHandler<ChatCreatedDomainEvent>
    {
        public ChatCreatedDomainEventHandler()
        {

        }

        public Task Handle(ChatCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
