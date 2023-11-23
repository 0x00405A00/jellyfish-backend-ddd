using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.UpdateChat
{
    public record UpdateChatCommand(Guid UpdatedByUserId, Guid ChatId, string? ChatName,string? ChatDescription, string? Picture, string? PictureMimeType) : ICommand<Guid>;
}
