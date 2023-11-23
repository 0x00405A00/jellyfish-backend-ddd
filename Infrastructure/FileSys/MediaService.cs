using Domain.Entities.Chats;
using Microsoft.Extensions.Logging;

namespace Infrastructure.FileSys
{
    /// <summary>
    /// Abstraction class to replace the IFileHandler in future with AzureBlob Storage to increase the performance
    /// That means that functions like GetProfilePicture or CreateProfilePicture will get/put their data to cloud instead of IFileHandler (on local filesystem)
    /// </summary>
    public class MediaService
    {
        private readonly ILogger<MediaService> logger;
        private readonly IFileHandler fileHandler;

        public MediaService(
            ILogger<MediaService> logger,
            IFileHandler fileHandler)
        {
            this.logger = logger;
            this.fileHandler = fileHandler;
        }
        public async Task<byte[]?> GetChatPicture(Guid chatId, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(chatId);
                return await fileHandler.ReadFile(path, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public async Task<string?> GetChatPictureBase64(Guid chatId, CancellationToken cancellationToken)
        {

            try
            {
                var result = await this.GetChatPicture(chatId, cancellationToken);
                var base64 = Convert.ToBase64String(result);
                return base64;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public string? CreateChatPicture(Guid chatId, byte[] content, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(chatId);
                fileHandler.CreateOrUpdateFile(path, content, cancellationToken);
                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;

        }
        public async Task<byte[]?> GetProfilePicture(Guid userId, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(userId);
                return await fileHandler.ReadFile(path, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public async Task<string?> GetProfilePictureBase64(Guid userId, CancellationToken cancellationToken)
        {

            try
            {
                var result = await this.GetProfilePicture(userId,cancellationToken);
                var base64 = Convert.ToBase64String(result);
                return base64;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public string? CreateProfilePicture(Guid userId, byte[] content, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(userId);
                fileHandler.CreateOrUpdateFile(path, content, cancellationToken);
                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;

        }
        public string? DeleteProfilePicture(Guid userId, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(userId);
                fileHandler.DeleteFile(path, cancellationToken);
                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;

        }
        public string GetProfilePicturePath(Guid userId)
        {
            return Path.Combine(FileHandler.UserProfilePictures, userId.ToString());
        }
        public string GetChatPicturePath(Guid chatId)
        {
            return Path.Combine(FileHandler.ChatPictures, chatId.ToString());
        }
    }
}
