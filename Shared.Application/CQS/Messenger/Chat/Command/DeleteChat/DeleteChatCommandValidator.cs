using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.DeleteChat
{
    public class DeleteChatCommandValidator : AbstractValidator<DeleteChatCommand>
    {
        public DeleteChatCommandValidator()
        {
            RuleFor(command => command.DeletedByUserId).NotEmpty().WithMessage("DeletedByUserId must not be empty.");
            RuleFor(command => command.ChatId).NotEmpty().WithMessage("ChatId must not be empty.");
        }
    }
}
