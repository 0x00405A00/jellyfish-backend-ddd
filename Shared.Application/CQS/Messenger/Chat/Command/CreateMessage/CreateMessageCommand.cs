using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage
{
    public record CreateMessageCommand(Guid ChatId,Guid MessageCreatorId,List<MessageDTO> Messages) : ICommand<List<MessageDTO>>;
}
