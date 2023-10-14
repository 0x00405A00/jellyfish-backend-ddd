using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.UpdateChat
{
    internal class UpdateChatCommandValidator : AbstractValidator<UpdateChatCommand>
    {
        public UpdateChatCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
