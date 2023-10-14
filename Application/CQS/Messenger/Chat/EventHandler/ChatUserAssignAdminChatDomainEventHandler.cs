using Domain.Entities.Chats.Event;
using Domain.Entities.User;
using Domain.Primitives;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatUserAssignAdminChatDomainEventHandler :
        INotificationHandler<ChatUserAssignAdminChatDomainEvent>
    {
        public ChatUserAssignAdminChatDomainEventHandler()
        {

        }

        public Task Handle(ChatUserAssignAdminChatDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
