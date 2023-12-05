using FluentValidation;

namespace Application.CQS.User.Commands.PasswordReset.Reset
{
    public class UserPasswordResetCommandValidator : AbstractValidator<UserPasswordResetCommand>
    {
        public UserPasswordResetCommandValidator()
        {
            RuleFor(command => command.PasswordResetBase64Token).NotEmpty().WithMessage("PasswordResetBase64Token must not be empty.");
            RuleFor(command => command.Password).NotEmpty().MinimumLength(6).WithMessage("Password must not be empty and have at least 6 characters.");
            RuleFor(command => command.PasswordConfirm).Equal(command => command.Password).WithMessage("Passwords do not match.");
            RuleFor(command => command.PasswordResetCode).NotEmpty().WithMessage("PasswordResetCode must not be empty.");
        }
    }
}
