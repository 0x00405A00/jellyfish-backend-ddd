using FluentValidation;

namespace Application.CQS.User.Commands.PasswordReset.Confirmation
{
    internal class UserPasswordResetRequestConfirmationCommandValidator : AbstractValidator<UserPasswordResetRequestConfirmationCommand>
    {
        public UserPasswordResetRequestConfirmationCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
