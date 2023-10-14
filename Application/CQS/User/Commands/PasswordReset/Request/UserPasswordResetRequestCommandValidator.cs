using FluentValidation;

namespace Application.CQS.User.Commands.PasswordReset.Request
{
    internal class UserPasswordResetRequestCommandValidator : AbstractValidator<UserPasswordResetRequestCommand>
    {
        public UserPasswordResetRequestCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
