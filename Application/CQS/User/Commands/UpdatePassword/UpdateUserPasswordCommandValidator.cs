using FluentValidation;

namespace Application.CQS.User.Commands.UpdatePassword
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(command => command.UpdatedBy).NotEmpty().WithMessage("UpdatedBy must not be empty.");
            RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId must not be empty.");
            RuleFor(command => command.Password).NotEmpty().MinimumLength(6).WithMessage("Password must not be empty and have at least 6 characters.");
            RuleFor(command => command.PasswordConfirm).Equal(command => command.Password).WithMessage("Passwords do not match.");
        }
    }
}
