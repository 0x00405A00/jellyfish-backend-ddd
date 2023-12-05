using FluentValidation;

namespace Application.CQS.User.Commands.RegisterUser.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(command => command.UserName).NotEmpty().MaximumLength(255).WithMessage("UserName must not be empty and should not exceed 255 characters.");
            RuleFor(command => command.Password).NotEmpty().MinimumLength(6).WithMessage("Password must not be empty and have at least 6 characters.");
            RuleFor(command => command.PasswordRepeat).Equal(command => command.Password).WithMessage("Passwords do not match.");
            RuleFor(command => command.FirstName).NotEmpty().MaximumLength(255).WithMessage("FirstName must not be empty and should not exceed 255 characters.");
            RuleFor(command => command.LastName).NotEmpty().MaximumLength(255).WithMessage("LastName must not be empty and should not exceed 255 characters.");
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Invalid or empty email address.");
            RuleFor(command => command.Phone).NotEmpty().WithMessage("Phone must not be empty.");
            RuleFor(command => command.DateOfBirth).NotEmpty().WithMessage("DateOfBirth must not be empty.");
        }
    }
}
