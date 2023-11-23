using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.UpdateProfilePicture
{
    public record UploadProfilePictureCommand(
        Guid UserId,
        string Base64,
        string MimeType) : ICommand<bool>;
}
