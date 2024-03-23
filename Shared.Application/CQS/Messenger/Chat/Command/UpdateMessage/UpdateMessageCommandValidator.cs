using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.UpdateMessage
{
    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(command => command.ModifiedById).NotEmpty().WithMessage("ModifiedById must not be empty.");
            RuleFor(command => command.ChatId).NotEmpty().WithMessage("ChatId must not be empty.");
            RuleFor(command => command.MessageId).NotEmpty().WithMessage("MessageId must not be empty.");
        }
    }
}
