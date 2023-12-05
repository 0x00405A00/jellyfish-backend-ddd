using FluentValidation;

namespace Application.CQS.User.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.DeletedByUserId).NotEmpty().WithMessage("DeletedByUserId must not be empty.");
            RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId must not be empty.");
        }
    }
}
