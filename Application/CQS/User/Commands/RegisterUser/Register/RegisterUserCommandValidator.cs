using FluentValidation;

namespace Application.CQS.User.Commands.RegisterUser.Register
{
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
