using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.RegisterUser.Register
{
    public class RegisterUserCommand : ICommand<UserDTO>
    {
        public readonly string? UserName;
        public readonly string? Password;
        public readonly string? PasswordRepeat;
        public readonly string? FirstName;
        public readonly string? LastName;
        public readonly string? Email;
        public readonly string? Phone;
        public readonly DateTime DateOfBirth;

        public RegisterUserCommand(string? UserName, string? Password, string? PasswordRepeat, string? FirstName, string? LastName, string? Email, string? Phone, DateTime DateOfBirth)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.PasswordRepeat = PasswordRepeat;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.DateOfBirth = DateOfBirth;
        }
    }
}