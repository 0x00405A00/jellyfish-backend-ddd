using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.RemoveFriendshipRequest
{
    /// <summary>
    /// Remove Friendship Request Command Record
    /// </summary>
    /// <param name="ExecuteUserId">The operative user uuid that is decline the friendship request</param>
    /// <param name="FriendshipRequestId">The friendship request uuid that the executeUserUuid is decline</param>
    public record RemoveFriendshipRequestCommand(Guid ExecuteUserId,Guid FriendshipRequestId) : ICommand<bool>;
}
