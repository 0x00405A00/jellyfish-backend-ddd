using FluentValidation;

namespace Application.CQS.User.Commands.PasswordReset.Reset
{
    internal class UserPasswordResetCommandValidator : AbstractValidator<UserPasswordResetCommand>
    {
        public UserPasswordResetCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
