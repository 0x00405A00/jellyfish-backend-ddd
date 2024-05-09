namespace Domain.ValueObjects
{
    public class Picture : Media
    { 
        private Picture():base()
        {

        }
        public Picture(byte[] data, string filePath, string fileExtension) : base(data, filePath, fileExtension)
        {
        }
        public Picture(string filePath, string fileExtension) : this(null,filePath, fileExtension)
        {
        }
        public static new Picture Parse(byte[] data, string filePath, string fileExtension)
        {
            return new Picture(data, filePath, fileExtension);
        }
        public static new Picture Parse(string filePath, string fileExtension)
        {
            return new Picture(filePath, fileExtension);
        }
        public static new Picture Parse(byte[] data, string mimeType)
        {
            return new Picture(data, null, mimeType);
        }
    }
}
