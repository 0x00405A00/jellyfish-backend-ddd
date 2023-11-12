using FluentValidation;

namespace Application.CQS.Auth.Command.RefreshAuth
{
    internal class RefreshAuthCommandValidator : AbstractValidator<RefreshAuthCommand>
    {
        public RefreshAuthCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
