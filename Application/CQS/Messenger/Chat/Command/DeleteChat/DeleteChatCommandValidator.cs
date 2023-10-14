using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.DeleteChat
{
    internal class DeleteChatCommandValidator : AbstractValidator<DeleteChatCommand>
    {
        public DeleteChatCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
