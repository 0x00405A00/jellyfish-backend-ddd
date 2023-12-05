using FluentValidation;
using Shared.MimePart;

namespace Application.CQS.User.Commands.UpdateProfilePicture
{
    public class UploadProfilePictureCommandValidator : AbstractValidator<UploadProfilePictureCommand>
    {
        public UploadProfilePictureCommandValidator()
        {
            RuleFor(command => command.UpdatedBy).NotEmpty().WithMessage("UpdatedBy must not be empty.");
            RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId must not be empty.");
            RuleFor(command => command.Base64).NotEmpty().WithMessage("Base64 must not be empty.");
            RuleFor(command => command.MimeType).NotEmpty().WithMessage("MimeType must not be empty.");
            RuleFor(command => command.MimeType).Must(MimeExtension.IsValidMimeTypeForMediaContent).When(command => !string.IsNullOrEmpty(command.Base64))
                .WithMessage("Invalid MimeType when Base64 is provided.");
        }
    }
}
