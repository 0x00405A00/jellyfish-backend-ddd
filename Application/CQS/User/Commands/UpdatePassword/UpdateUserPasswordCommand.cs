using Application.Abstractions.Messaging;
using Domain.Entities.User;
using Domain.ValueObjects;

namespace Application.CQS.User.Commands.UpdatePassword
{
    /// <summary>
    /// Update User Command Record
    /// </summary>
    /// <param name="UserId">User id that should be updated</param>
    /// <param name="Password"><Update property 'Password'/param>
    /// <param name="PasswordConfirm"><Update property 'PasswordConfirm'/param>
    public record UpdateUserPasswordCommand(Guid UserId,
                                    string? Password,
                                    string? PasswordConfirm) : ICommand<Guid>;
}
