using FluentValidation;

namespace Application.CQS.User.Commands.CreateUser
{
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
