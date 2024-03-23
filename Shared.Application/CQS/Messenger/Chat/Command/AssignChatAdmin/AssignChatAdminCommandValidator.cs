using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.AssignChatAdmin
{
    public class AssignChatAdminCommandValidator : AbstractValidator<AssignChatAdminCommand>
    {
        public AssignChatAdminCommandValidator()
        {
        }
    }
}
