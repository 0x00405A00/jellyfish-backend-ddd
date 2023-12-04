using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.DeleteMessage
{
    internal class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
        }
    }
}
