using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.DeleteMessage
{
    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(command => command.DeletedById).NotEmpty().WithMessage("DeletedById must not be empty.");
            RuleFor(command => command.ChatId).NotEmpty().WithMessage("ChatId must not be empty.");
            RuleFor(command => command.MessageId).NotEmpty().WithMessage("MessageId must not be empty.");
        }
    }
}
