using Domain.Entities.Chats.Event;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.DeleteChat.EventHandler
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
