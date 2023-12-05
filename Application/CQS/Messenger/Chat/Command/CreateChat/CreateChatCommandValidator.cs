using FluentValidation;
using Shared.MimePart;

namespace Application.CQS.Messenger.Chat.Command.CreateChat
{
    public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
            RuleFor(command => command.ChatOwnerUuid).NotEmpty().WithMessage("ChatOwnerUuid should not be empty.");
            RuleFor(command => command.ChatName).NotEmpty().WithMessage("ChatName should not be empty.").MaximumLength(255).WithMessage("ChatName should not be greater than 255 chars.");
            RuleFor(command => command.Members).NotEmpty().WithMessage("Members should not be empty.");
            RuleFor(command => command.Members).Must(members => members != null && members.Count > 0).WithMessage("Members should not be empty.");
            RuleFor(command => command.PictureMimeType).Must(MimeExtension.IsValidMimeTypeForMediaContent).When(command => !string.IsNullOrEmpty(command.Picture)).WithMessage("Invalid picture mime type.");
        }
    }
}
