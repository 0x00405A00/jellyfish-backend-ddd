using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.CreateChat
{
    public record CreateChatCommand(Guid ChatOwnerUuid, string ChatName,string? ChatDescription, List<Guid> Members, string? Picture,string ?PictureMimeType) : ICommand<ChatDTO>;
}
