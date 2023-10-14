namespace Domain.ValueObjects
{
    public class MediaContent
    {
        public byte[] Data { get;private set; }
        public MediaContent(byte[] data)
        {
            Data = data;    
        }
        public static MediaContent Parse(byte[] data)
        {
            return new MediaContent(data);
        }
    }
}
