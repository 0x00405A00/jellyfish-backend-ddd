using FluentValidation;
using Shared.MimePart;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage
{
    internal class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(x => x.Messages.FindAll(y => y.HasBase64ContentSet)
                                   .All(x => MimeExtension.IsValidMimeTypeForMediaContent(x.BinaryContentMimeType)));
        }
    }
}
