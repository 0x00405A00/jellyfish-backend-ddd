namespace Domain.ValueObjects
{
    public class MediaContent
    {
        public byte[]? Data { get; private set; }
        public string FilePath { get; private set; }
        /// <summary>
        /// Filesystem Fileextension or MimeType
        /// </summary>
        public string FileExtension { get; private set; }

        protected MediaContent()
        {

        }
        public MediaContent(byte[] data, string filePath, string fileExtension)
        {
            Data = data;
            FilePath = filePath;
            FileExtension = fileExtension;
        }
        public MediaContent(string filePath, string fileExtension):this(null,filePath,fileExtension)
        {
        }

        public static MediaContent Parse(byte[] data, string filePath, string fileExtension)
        {
            return new MediaContent(data, filePath, fileExtension);
        }
        public static MediaContent Parse(byte[] data, string mimeType)
        {
            return new MediaContent(data, null, mimeType);
        }
        public static MediaContent ParseBase64(string data, string mimeType)
        {
            byte[] bin = null;
            try
            {
                bin = Convert.FromBase64String(data);
            }
            catch (Exception e)
            {
                throw;
            }
            return new MediaContent(bin, null, mimeType);
        }
        public static MediaContent Parse(string filePath, string fileExtension)
        {
            return new MediaContent(filePath, fileExtension);
        }
        public void SetBinary(byte[] data)=> Data= data;    
        public override string ToString()
        {
            return Data == null ? null: Convert.ToBase64String(Data);
        }
    }
}
