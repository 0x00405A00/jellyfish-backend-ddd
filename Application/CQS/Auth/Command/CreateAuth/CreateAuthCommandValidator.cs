using FluentValidation;

namespace Application.CQS.Auth.Command.CreateAuth
{
    public class CreateAuthCommandValidator : AbstractValidator<CreateAuthCommand>
    {
        public CreateAuthCommandValidator()
        {
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Invalid or empty email address.");
            RuleFor(command => command.Password).NotEmpty().MinimumLength(6).WithMessage("Password must not be empty and have at least 6 characters.");
            RuleFor(command => command.LocalIp).NotEmpty().WithMessage("Local IP must not be empty.");
            RuleFor(command => command.LocalIpPort).InclusiveBetween(0, 65535).WithMessage("Local IP Port must be between 0 and 65535.");
            RuleFor(command => command.RemoteIp).NotEmpty().WithMessage("Remote IP must not be empty.");
            RuleFor(command => command.RemoteIpPort).InclusiveBetween(0, 65535).WithMessage("Remote IP Port must be between 0 and 65535.");
            RuleFor(command => command.UserAgent).NotEmpty().WithMessage("UserAgent must not be empty.");
        }
    }
}
