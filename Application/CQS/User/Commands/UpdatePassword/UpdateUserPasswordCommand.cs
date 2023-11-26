using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.UpdatePassword
{
    /// <summary>
    /// Update User Command Record
    /// </summary>
    /// <param name="UserId">User id that should be updated</param>
    /// <param name="Password"><Update property 'Password'/param>
    /// <param name="PasswordConfirm"><Update property 'PasswordConfirm'/param>
    public record UpdateUserPasswordCommand(Guid UpdatedBy, Guid UserId,
                                    string? Password,
                                    string? PasswordConfirm) : ICommand<Guid>;
}
