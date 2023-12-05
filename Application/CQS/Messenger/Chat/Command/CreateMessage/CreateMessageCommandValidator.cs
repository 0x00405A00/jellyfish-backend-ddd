using FluentValidation;
using Shared.DataTransferObject.Messenger;
using Shared.MimePart;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(command => command.ChatId).NotEmpty().WithMessage("ChatId must not be empty.");
            RuleFor(command => command.MessageCreatorId).NotEmpty().WithMessage("MessageCreatorId must not be empty.");
            RuleFor(command => command.Messages).NotEmpty().WithMessage("The list of messages must not be empty.");
            RuleForEach(command => command.Messages)
                .SetValidator(new MessageDTOValidator());
        }
        public class MessageDTOValidator : AbstractValidator<MessageDTO>
        {
            public MessageDTOValidator()
            {
                RuleFor(message => message.Uuid).NotEmpty().WithMessage("Uuid must not be empty.");
                RuleFor(message => message.ChatId).NotEmpty().WithMessage("ChatId must not be empty.");
                RuleFor(message => message.OwnerUuid).NotEmpty().WithMessage("OwnerUuid must not be empty.");
                RuleFor(message => message.Text).NotEmpty().When(message => string.IsNullOrEmpty(message.BinaryContentBase64))
                    .WithMessage("The text of the message must not be empty unless BinaryContentBase64 is set.");
                RuleFor(message => message.BinaryContentBase64).NotEmpty().When(message => string.IsNullOrEmpty(message.Text))
                    .WithMessage("The BinaryContentBase64 of the message must not be empty unless Text is set.");
                RuleFor(message => message.BinaryContentMimeType).NotEmpty().When(message => !string.IsNullOrEmpty(message.BinaryContentBase64))
                    .WithMessage("The BinaryContentMimeType of the message must not be empty when BinaryContentBase64 is set.");
                RuleFor(command => command.BinaryContentMimeType).Must(MimeExtension.IsValidMimeTypeForMediaContent).When(command => !string.IsNullOrEmpty(command.BinaryContentBase64))
                    .WithMessage("Invalid PictureMimeType when Picture is provided.");

            }
        }
    }
}
