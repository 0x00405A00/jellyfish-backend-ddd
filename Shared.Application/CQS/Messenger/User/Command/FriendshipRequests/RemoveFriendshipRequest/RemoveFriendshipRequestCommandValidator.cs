using FluentValidation;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.RemoveFriendshipRequest
{
    internal class RemoveFriendshipRequestCommandValidator : AbstractValidator<RemoveFriendshipRequestCommand>
    {
        public RemoveFriendshipRequestCommandValidator()
        {
        }
    }
}
