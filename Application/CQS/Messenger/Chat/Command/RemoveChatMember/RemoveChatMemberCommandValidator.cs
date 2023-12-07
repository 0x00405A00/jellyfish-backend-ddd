using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.RemoveChatMember
{
    public class RemoveChatMemberCommandValidator : AbstractValidator<RemoveChatMemberCommand>
    {
        public RemoveChatMemberCommandValidator()
        {
            
        }
    }
}
