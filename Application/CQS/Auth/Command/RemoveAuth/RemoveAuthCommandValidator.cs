using FluentValidation;

namespace Application.CQS.Auth.Command.RemoveAuth
{
    internal class RemoveAuthCommandValidator : AbstractValidator<RemoveAuthCommand>
    {
        public RemoveAuthCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
