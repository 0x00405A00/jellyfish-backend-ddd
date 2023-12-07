using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.RevokeChatAdmin
{
    public class RevokeChatAdminCommandValidator : AbstractValidator<RevokeChatAdminCommand>
    {
        public RevokeChatAdminCommandValidator()
        {
        }
    }
}
