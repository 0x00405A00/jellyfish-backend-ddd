namespace Infrastructure.FileSys
{
    public interface IFileHandler
    {
        internal void CreateApplicationFolders();
        internal Task<MediaContentDTO> CreateOrUpdateFile(string path, byte[] content, CancellationToken cancellationToken);
        internal void DeleteFile(string path, CancellationToken cancellationToken);
        internal Task<byte[]> ReadFile(string path, CancellationToken cancellationToken);
    }
}