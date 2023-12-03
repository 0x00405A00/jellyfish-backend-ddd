using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.RemoveFriendshipRequest
{
    /// <summary>
    /// Remove Friendship Request Command Record
    /// Give the user that create the friendship request the opportunity to revoke the request to user
    /// </summary>
    /// <param name="ExecutorUserId">The user that perform the RemoveFriendshipRequestCommand</param>
    /// <param name="RequestUserId">The request user of friendship request</param>
    /// <param name="TargetUserId">The target user of friendship request</param>
    public record RemoveFriendshipRequestCommand(Guid ExecutorUserId,Guid RequestUserId,Guid TargetUserId) : ICommand<Guid>;
}
