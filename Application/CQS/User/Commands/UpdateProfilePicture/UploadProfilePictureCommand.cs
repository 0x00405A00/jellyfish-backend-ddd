using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.UpdateProfilePicture
{
    public record UploadProfilePictureCommand(
        Guid UpdatedBy,
        Guid UserId,
        string Base64,
        string MimeType) : ICommand<UserDTO>;
}
