﻿using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.UpdateUser
{
    /// <summary>
    /// Update User Command Record
    /// </summary>
    /// <param name="UserId">User id that should be updated</param>
    /// <param name="UserName">Update property 'UserName'</param>
    /// <param name="Password"><Update property 'Password'/param>
    /// <param name="FirstName">Update property 'FirstName'</param>
    /// <param name="LastName">Update property 'LastName'</param>
    /// <param name="Email">Update property 'Email'</param>
    /// <param name="Phone">Update property 'Phone'</param>
    /// <param name="DateOfBirth">Update property 'DateOfBirth'</param>
    public record UpdateUserCommand(Guid UpdatedBy, Guid UserId,
                                    string? UserName,
                                    string? Password,
                                    string? PasswordConfirm,
                                    string? FirstName,
                                    string? LastName,
                                    string? Email,
                                    string? Phone,
                                    DateTime? DateOfBirth) : ICommand<UserDTO>;
}
