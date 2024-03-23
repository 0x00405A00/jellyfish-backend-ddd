using Domain.Entities.Chats.Events;
using Infrastructure.SignalR;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Backend.SignalR;

namespace Application.CQS.Messenger.Chat.EventHandler
{
    internal sealed class ChatAppendMessageDomainEventHandler :
        INotificationHandler<ChatAppendMessageDomainEvent>
    {
        private readonly IServiceProvider serviceProvider;

        public ChatAppendMessageDomainEventHandler(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Handle(ChatAppendMessageDomainEvent notification, CancellationToken cancellationToken)
        {

        }
    }
}
