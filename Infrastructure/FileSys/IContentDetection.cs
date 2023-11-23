namespace Infrastructure.FileSys
{
    public interface IContentDetection
    {
        byte[] BinaryData { get; }
        void SetBinary(byte[] binaryData);
    }
}
