using Domain.Entities.Messages.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage.EventHandler
{
    internal sealed class MessageCreatedDomainEventHandler :
        INotificationHandler<MessageCreatedDomainEvent>
    {
        public MessageCreatedDomainEventHandler()
        {

        }

        public Task Handle(MessageCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
