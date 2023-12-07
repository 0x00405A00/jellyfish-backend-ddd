using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.AddChatMember
{
    public class AddChatMemberCommandValidator : AbstractValidator<AddChatMemberCommand>
    {
        public AddChatMemberCommandValidator()
        {
        }
    }
}
