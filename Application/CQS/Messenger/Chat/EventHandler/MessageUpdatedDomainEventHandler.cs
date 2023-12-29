using Domain.Entities.Messages.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class MessageUpdatedDomainEventHandler :
        INotificationHandler<MessageUpdatedDomainEvent>
    {
        public MessageUpdatedDomainEventHandler()
        {

        }

        public Task Handle(MessageUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
