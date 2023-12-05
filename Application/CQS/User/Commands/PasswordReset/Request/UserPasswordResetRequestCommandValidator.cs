using FluentValidation;

namespace Application.CQS.User.Commands.PasswordReset.Request
{
    public class UserPasswordResetRequestCommandValidator : AbstractValidator<UserPasswordResetRequestCommand>
    {
        public UserPasswordResetRequestCommandValidator()
        {
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Invalid or empty email address.");
        }
    }
}
