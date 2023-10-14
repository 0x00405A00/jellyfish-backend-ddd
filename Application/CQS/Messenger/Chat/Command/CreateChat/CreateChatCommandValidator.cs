using FluentValidation;

namespace Application.CQS.Messenger.Chat.Command.CreateChat
{
    internal class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator()
        {
        }
    }
}
