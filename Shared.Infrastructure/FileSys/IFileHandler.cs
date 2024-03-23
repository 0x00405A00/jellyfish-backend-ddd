namespace Shared.Infrastructure.FileSys
{
    public interface IFileHandler
    {
        public void CreateApplicationFolders();
        public Task<MediaContentDTO> CreateOrUpdateFile(string path, byte[] content, CancellationToken cancellationToken);
        public void DeleteFile(string path, CancellationToken cancellationToken);
        public Task<byte[]> ReadFile(string path, CancellationToken cancellationToken);
    }
}