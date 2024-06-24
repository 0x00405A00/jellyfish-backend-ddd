using FluentValidation;

namespace Application.CQS.Auth.Command.CreateAuth
{
    public class CreateAuthCommandValidator : AbstractValidator<CreateAuthCommand>
    {
        public CreateAuthCommandValidator()
        {
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Invalid or empty email address.");
            RuleFor(command => command.Password).NotEmpty().MinimumLength(6).WithMessage("Password must not be empty and have at least 6 characters.");
            RuleFor(command => command.UserAgent).NotEmpty().WithMessage("UserAgent must not be empty.");
        }
    }
}
