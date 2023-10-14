using Application.CQS.User.Commands.CreateUser;
using FluentValidation;

namespace Application.CQS.Auth.Command.CreateAuth
{
    internal class CreateAuthCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateAuthCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
