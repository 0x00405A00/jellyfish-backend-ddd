using FluentValidation;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    public class UserActivationCommandValidator : AbstractValidator<UserActivationCommand>
    {
        public UserActivationCommandValidator()
        {
            RuleFor(command => command.Base64Token).NotEmpty().WithMessage("Base64Token must not be empty.");
            RuleFor(command => command.ActivationCode).NotEmpty().WithMessage("ActivationCode must not be empty.");
        }
    }
}
