using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.UpdateChat
{
    public record UpdateChatCommand(
        Guid UpdatedByUserId,
        Guid ChatId,
        string? ChatName,
        string? ChatDescription,
        string? Picture,
        string? PictureMimeType) : ICommand<ChatDTO>;
}
