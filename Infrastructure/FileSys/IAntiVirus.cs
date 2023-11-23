namespace Infrastructure.FileSys
{

    /// <summary>
    /// Abstraction for NClam conrete implementation
    /// </summary>
    /// <param name="binaryData"></param>
    /// <returns></returns>
    public interface IAntiVirus : IContentDetection
    {
        /// <summary>
        /// Abstraction for NClam conrete implementation
        /// </summary>
        /// <param name="binaryData">Binary that should be checked for virus content</param>
        /// <returns></returns>
        Task<bool> CheckIfBinaryIsVirusContent();
    }
}
