using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.CreateFriendshipRequest
{
    /// <summary>
    /// Create Friend Request Command Record
    /// </summary>
    /// <param name="RequesterUserId">The user that request the friendship invitation to PossibleNewFriendUuid</param>
    /// <param name="PossibleNewFriendId">The target id</param>
    public record CreateFriendshipRequestCommand(Guid RequesterUserId,Guid PossibleNewFriendId,string? Message) : ICommand<FriendshipRequestDTO>;
}
