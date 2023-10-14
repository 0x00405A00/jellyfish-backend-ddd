using FluentValidation;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest
{
    internal class AcceptFriendshipRequestCommandValidator : AbstractValidator<AcceptFriendshipRequestCommand>
    {
        public AcceptFriendshipRequestCommandValidator()
        {

        }
    }
}
