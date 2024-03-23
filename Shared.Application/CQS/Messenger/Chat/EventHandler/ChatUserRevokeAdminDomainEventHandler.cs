using Domain.Entities.Chats.Events;
using MediatR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatUserRevokeAdminDomainEventHandler :
        INotificationHandler<ChatUserRevokeAdminDomainEvent>
    {
        public ChatUserRevokeAdminDomainEventHandler()
        {

        }

        public Task Handle(ChatUserRevokeAdminDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
