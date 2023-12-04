using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.UpdateMessage
{
    internal class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
        }
    }
}
