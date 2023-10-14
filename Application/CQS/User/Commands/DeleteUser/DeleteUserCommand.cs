using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.DeleteUser
{
    /// <summary>
    /// Delete User Command
    /// </summary>
    /// <param name="DeletedByUserId">User that perform the Operation</param>
    /// <param name="UserId">User that should be deleted</param>
    public record DeleteUserCommand(Guid DeletedByUserId, Guid UserId) : ICommand<Guid>;
}
