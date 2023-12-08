
namespace Infrastructure.FileSys
{
    public interface IMediaService
    {
        Task<MediaContentDTO> CheckContent(MediaContentDTO mediaContentDTO, CancellationToken cancellationToken);
        string? CreateChatPicture(Guid chatId, string fileExtension, byte[] content, CancellationToken cancellationToken);
        string? CreateProfilePicture(Guid userId, string fileExtension, byte[] content, CancellationToken cancellationToken);
        string? DeleteProfilePicture(Guid userId, string fileExtension, CancellationToken cancellationToken);
        Task<byte[]?> GetChatPicture(Guid chatId, string fileExtension, CancellationToken cancellationToken);
        Task<string?> GetChatPictureBase64(Guid chatId, string fileExtension, CancellationToken cancellationToken);
        string GetChatPicturePath(Guid chatId, string fileExtension);
        Task<byte[]?> GetProfilePicture(Guid userId, string fileExtension, CancellationToken cancellationToken);
        Task<string?> GetProfilePictureBase64(Guid userId, string fileExtension, CancellationToken cancellationToken);
        string GetProfilePicturePath(Guid userId, string fileExtension);
    }
}