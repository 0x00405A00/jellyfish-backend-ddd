using Application.Abstractions.Messaging;
using Application.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Queries.GetChatById
{
    public sealed record GetChatByIdQuery(Guid Id) : IQuery<ChatDTO>;
}
