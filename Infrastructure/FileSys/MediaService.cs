using Domain.Entities.Chats;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System.Threading;

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
        private readonly IAntiVirus antiVirus;
        private readonly IAzureAdultContentDetection azureAdultContentDetection;

        public MediaService(
            ILogger<MediaService> logger,
            IFileHandler fileHandler, 
            IAntiVirus antiVirus,
            IAzureAdultContentDetection azureAdultContentDetection)
        {
            this.logger = logger;
            this.fileHandler = fileHandler;
            this.antiVirus = antiVirus;
            this.azureAdultContentDetection = azureAdultContentDetection;
        }
        #region User Profile Picture
        public async Task<byte[]?> GetProfilePicture(Guid userId, string fileExtension, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(userId, fileExtension);
                return await fileHandler.ReadFile(path, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public async Task<string?> GetProfilePictureBase64(Guid userId, string fileExtension, CancellationToken cancellationToken)
        {

            try
            {
                var result = await this.GetProfilePicture(userId,fileExtension,cancellationToken);
                var base64 = Convert.ToBase64String(result);
                return base64;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public string? CreateProfilePicture(Guid userId, string fileExtension, byte[] content, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(userId, fileExtension);
                fileHandler.CreateOrUpdateFile(path, content, cancellationToken);
                return path.Replace(FileHandler.WebRoot, "");
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;

        }
        public string? DeleteProfilePicture(Guid userId, string fileExtension, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(userId, fileExtension);
                fileHandler.DeleteFile(path, cancellationToken);
                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;

        }
        public string GetProfilePicturePath(Guid userId,string fileExtension)
        {
            return Path.Combine(FileHandler.UserProfilePictures, userId.ToString()+ fileExtension);
        }
        public string GetChatPicturePath(Guid chatId, string fileExtension)
        {
            return Path.Combine(FileHandler.ChatPictures, chatId.ToString() + fileExtension);
        }
        #endregion
        #region Chat Picture

        public async Task<byte[]?> GetChatPicture(Guid chatId, string fileExtension, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(chatId, fileExtension);
                return await fileHandler.ReadFile(path, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public async Task<string?> GetChatPictureBase64(Guid chatId, string fileExtension, CancellationToken cancellationToken)
        {

            try
            {
                var result = await this.GetChatPicture(chatId, fileExtension, cancellationToken);
                var base64 = Convert.ToBase64String(result);
                return base64;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public string? CreateChatPicture(Guid chatId, string fileExtension, byte[] content, CancellationToken cancellationToken)
        {

            try
            {
                string path = GetProfilePicturePath(chatId, fileExtension);
                fileHandler.CreateOrUpdateFile(path, content, cancellationToken);
                return path.Replace(FileHandler.WebRoot, "");
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;

        }

        #endregion
        
        public async Task<MediaContentDTO> CheckContent(MediaContentDTO mediaContentDTO,CancellationToken cancellationToken)
        {
            return await mediaContentDTO.CheckInadmissibleContent(antiVirus, azureAdultContentDetection, cancellationToken);
        }
    }
}
