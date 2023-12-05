using FluentValidation;

namespace Application.CQS.Auth.Command.RemoveAuth
{
    public class RemoveAuthCommandValidator : AbstractValidator<RemoveAuthCommand>
    {
        public RemoveAuthCommandValidator()
        {
            RuleFor(command => command.JwtBearer).NotEmpty().WithMessage("JwtBearer must not be empty.");
        }
    }
}
