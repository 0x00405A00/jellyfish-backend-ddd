namespace Infrastructure.FileSys
{
    /// <summary>
    /// Abstraction for Azure AI Vision services, the method signatures below must be implemented by conrete implemenation of 'IAzureAdultContentDetection'
    /// </summary>
    public interface IAzureAdultContentDetection : IContentDetection
    {
        Task<bool> CheckIfBinaryIsNotPornContent();
        Task<bool> CheckIfBinaryIsNotViolentContent();
    }
}
