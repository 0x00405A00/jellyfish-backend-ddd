using FluentValidation;

namespace Application.CQS.Auth.Command.CreateAuth
{
    internal class CreateAuthCommandValidator : AbstractValidator<CreateAuthCommand>
    {
        public CreateAuthCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
