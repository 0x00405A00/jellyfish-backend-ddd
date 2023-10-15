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
        public override string ToString()
        {
            return Data == null ? null: Convert.ToBase64String(Data);
        }
    }
}
