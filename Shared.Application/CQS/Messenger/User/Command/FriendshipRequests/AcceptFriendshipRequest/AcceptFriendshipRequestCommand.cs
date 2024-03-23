using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest
{
    /// <summary>
    /// Accept the friendship 
    /// </summary>
    /// <param name="ExecutorUserId">The user that perform the RemoveFriendshipRequestCommand</param>
    /// <param name="RequestUserId"></param>
    /// <param name="TargetUserId"></param>
    public record AcceptFriendshipRequestCommand(Guid ExecutorUserId, Guid RequestUserId, Guid TargetUserId) : ICommand<bool>;
}
