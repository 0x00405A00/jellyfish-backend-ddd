using FluentValidation;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.CreateFriendshipRequest
{
    internal class CreateFriendshipRequestCommandValidator : AbstractValidator<CreateFriendshipRequestCommand>
    {
        public CreateFriendshipRequestCommandValidator()
        {

        }
    }
}
