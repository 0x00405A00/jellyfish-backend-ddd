using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest
{
    /// <summary>
    /// Accept Friend Request Command Record
    /// </summary>
    /// <param name="AcceptExecuteUserId">The User that accept the friendship request</param>
    /// <param name="FriendShipRequestId">The request uuid</param>
    public record AcceptFriendshipRequestCommand(Guid AcceptExecuteUserId,Guid FriendShipRequestId) : ICommand<bool>;
}
