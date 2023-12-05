using FluentValidation;

namespace Application.CQS.User.Commands.DeleteProfilePicture
{
    public class DeleteProfilePictureCommandValidator : AbstractValidator<DeleteProfilePictureCommand>
    {
        public DeleteProfilePictureCommandValidator()
        {
            RuleFor(command => command.DeletedBy).NotEmpty().WithMessage("DeletedBy must not be empty.");
            RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId must not be empty.");
        }
    }
}
