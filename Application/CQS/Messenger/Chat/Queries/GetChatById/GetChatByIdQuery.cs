using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Queries.GetChatById
{
    public sealed record GetChatByIdQuery(Guid Id) : IQuery<ChatDTO>;
}
