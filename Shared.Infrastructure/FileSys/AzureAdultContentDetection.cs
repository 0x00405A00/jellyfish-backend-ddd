using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Shared.Infrastructure.FileSys
{
    public class AzureAdultContentDetection : IAzureAdultContentDetection
    {
        private byte[] _binaryData = null;
        private readonly ILogger<AzureAdultContentDetection> logger;
        private readonly IConfiguration configuration;

        public byte[] BinaryData => _binaryData;
        public AzureAdultContentDetection(
            ILogger<AzureAdultContentDetection> logger,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        public async Task<bool> CheckIfBinaryIsNotPornContent()
        {
            return true;
        }

        public async Task<bool> CheckIfBinaryIsNotViolentContent()
        {
            return true;
        }

        public void SetBinary(byte[] binaryData)
        {
            _binaryData = binaryData;
        }
    }
}
