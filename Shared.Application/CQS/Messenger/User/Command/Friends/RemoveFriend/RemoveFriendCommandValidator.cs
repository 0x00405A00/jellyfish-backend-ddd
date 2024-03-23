using FluentValidation;

namespace Application.CQS.Messenger.User.Command.Friends.RemoveFriend
{
    internal class RemoveFriendCommandValidator : AbstractValidator<RemoveFriendCommand>
    {
        public RemoveFriendCommandValidator()
        {

        }
    }
}
