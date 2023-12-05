using FluentValidation;

namespace Application.CQS.Auth.Command.RefreshAuth
{
    public class RefreshAuthCommandValidator : AbstractValidator<RefreshAuthCommand>
    {
        public RefreshAuthCommandValidator()
        {
            RuleFor(command => command.Token).NotEmpty().WithMessage("Token must not be empty.");
            RuleFor(command => command.RefreshToken).NotEmpty().WithMessage("RefreshToken must not be empty.");
        }
    }
}
