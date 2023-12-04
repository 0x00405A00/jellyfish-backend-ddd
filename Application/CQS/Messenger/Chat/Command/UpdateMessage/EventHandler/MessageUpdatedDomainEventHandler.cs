using Domain.Entities.Message.Event;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage.EventHandler
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
