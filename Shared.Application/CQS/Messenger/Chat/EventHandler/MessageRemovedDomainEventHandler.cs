using Domain.Entities.Messages.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class MessageRemovedDomainEventHandler :
        INotificationHandler<MessageRemovedDomainEvent>
    {
        public MessageRemovedDomainEventHandler()
        {

        }

        public Task Handle(MessageRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
