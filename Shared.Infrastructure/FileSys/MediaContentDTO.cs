namespace Shared.Infrastructure.FileSys
{
    public sealed class MediaContentDTO
    {
        private byte[] _binary;
        private readonly IContentDetection[] contentDetection;
        private bool? _isNotPornContent;  // Porn content is not allowed, when true media will be deleted, check via https://learn.microsoft.com/en-us/azure/ai-services/computer-vision/concept-detecting-adult-content
        private bool? _isNoVirusContent;  // Viruses are not allowed, when true media will be deleted, check via NClam
        private bool? _isNotViolentContent;
        public bool IsNotPornContent => _isNotPornContent is not null && _isNotPornContent.Value;  // Porn content is not allowed, when true media will be deleted, check via https://learn.microsoft.com/en-us/azure/ai-services/computer-vision/concept-detecting-adult-content
        public bool IsNoVirusContent => _isNoVirusContent is not null && _isNoVirusContent.Value;  // Viruses are not allowed, when true media will be deleted, check via NClam
        public bool IsNotViolentContent => _isNotViolentContent is not null && _isNotViolentContent.Value; //Murder Content is not allowed, when true media will be deleted, check via https://learn.microsoft.com/en-us/azure/ai-services/computer-vision/concept-detecting-adult-content

        public bool AllChecksArePassed => _isNotPornContent is not null && _isNotViolentContent is not null && _isNoVirusContent is not null;
        public bool IsContentClean => IsNotPornContent && IsNoVirusContent && IsNotViolentContent;

        private MediaContentDTO(
            byte[] data,
            IContentDetection[] contentDetection)
        {
            _binary = data;
            this.contentDetection = contentDetection;
        }
        public static MediaContentDTO Parse(
            byte[] binaryData,
            params IContentDetection[] contentDetection)
        {
            contentDetection.ToList().ForEach(x=>x.SetBinary(binaryData));
            return new MediaContentDTO(binaryData,contentDetection);
        }
        public void PornCheckProcessed(bool resultFromCheck)
        {
            _isNotPornContent = resultFromCheck;
        }
        public void VirusCheckProcessed(bool resultFromCheck)
        {
            _isNotPornContent = resultFromCheck;
        }
        public void ViolentCheckProcessed(bool resultFromCheck)
        {
            _isNotViolentContent = resultFromCheck;
        }
        public byte[] GetBinaryData()
        {
            if (!AllChecksArePassed)
            {
                throw new MediaCheckNotCompletedException("media is not completly checked");
            }
            if (!IsContentClean)
            {
                throw new MediaViolatesPolicyException("media is inadmissible");
            }
            return _binary;
        }
    }
}
