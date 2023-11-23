using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.DeleteProfilePicture
{
    public record DeleteProfilePictureCommand(
        Guid UserId) : ICommand<bool>;
}
