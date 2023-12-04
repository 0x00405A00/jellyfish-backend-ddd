using FluentValidation;
using Shared.MimePart;

namespace Application.CQS.User.Commands.UpdateProfilePicture
{
    internal class UploadProfilePictureCommandValidator : AbstractValidator<UploadProfilePictureCommand>
    {
        public UploadProfilePictureCommandValidator()
        {
            RuleFor(x => !String.IsNullOrEmpty(x.Base64) && MimeExtension.IsValidMimeTypeForMediaContent(x.MimeType));
        }
    }
}
