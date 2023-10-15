using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.User.Commands.CreateUser
{
    public record CreateUserCommand(Guid CreatedBy,
                                    string UserName,
                                    string Password,
                                    string FirstName,
                                    string LastName,
                                    string Email,
                                    string Phone,
                                    DateTime DateOfBirth) : ICommand<UserDTO>;
}
