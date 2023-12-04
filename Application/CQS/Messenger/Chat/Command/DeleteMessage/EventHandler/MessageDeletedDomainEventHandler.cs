using Domain.Entities.Message.Event;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage.EventHandler
{
    internal sealed class MessageDeletedDomainEventHandler :
        INotificationHandler<MessageRemovedDomainEvent>
    {
        public MessageDeletedDomainEventHandler()
        {

        }

        public Task Handle(MessageRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
