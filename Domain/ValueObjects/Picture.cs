namespace Domain.ValueObjects
{
    public class Picture : MediaContent
    {
        public Picture(byte[] data) : base(data)
        {
        }
        public static new Picture Parse(byte[] data)
        {
            return new Picture(data);
        }
    }
}
