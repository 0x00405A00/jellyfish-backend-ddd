using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Shared.Infrastructure.FileSys
{
    public class AntiVirus : IAntiVirus
    {
        private byte[] _binaryData = null;
        private readonly ILogger<AntiVirus> logger;
        private readonly IConfiguration configuration;

        public byte[] BinaryData => _binaryData;

        public AntiVirus(
            ILogger<AntiVirus> logger,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        public async Task<bool> CheckIfBinaryIsNotVirusContent()
        {
            return true;
        }

        public void SetBinary(byte[] binaryData)
        {
            _binaryData = binaryData;
        }
    }
}
