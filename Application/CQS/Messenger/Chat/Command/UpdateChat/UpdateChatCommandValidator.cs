using FluentValidation;
using Shared.MimePart;

namespace Application.CQS.Messenger.Chat.Command.UpdateChat
{
    public class UpdateChatCommandValidator : AbstractValidator<UpdateChatCommand>
    {
        public UpdateChatCommandValidator()
        {
            RuleFor(command => command.UpdatedByUserId).NotEmpty().WithMessage("UpdatedByUserId must not be empty.");
            RuleFor(command => command.ChatId).NotEmpty().WithMessage("ChatId must not be empty.");
            RuleFor(command => command.ChatName).MaximumLength(255).WithMessage("ChatName must not exceed 255 characters.");
            RuleFor(command => command.PictureMimeType).Must(MimeExtension.IsValidMimeTypeForMediaContent).When(command => !string.IsNullOrEmpty(command.Picture))
                .WithMessage("Invalid PictureMimeType when Picture is provided.");
        }
    }
}
