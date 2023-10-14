using FluentValidation;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    internal class UserActivationCommandValidator : AbstractValidator<UserActivationCommand>
    {
        public UserActivationCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
