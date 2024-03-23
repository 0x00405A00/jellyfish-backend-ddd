using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.User.Command.Friends.RemoveFriend
{
    /// <summary>
    /// Remove Friend Command
    /// </summary>
    /// <param name="ExecuteUserId">The operative user uuid</param>
    /// <param name="TargetUserId">The user uuid that will be remove from friendlist of ExecuteUserId</param>
    public record RemoveFriendCommand(Guid ExecuteUserId,Guid TargetUserId) : ICommand<Guid>;
}
