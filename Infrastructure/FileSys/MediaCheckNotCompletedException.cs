using System.Runtime.Serialization;

namespace Infrastructure.FileSys
{
    [Serializable]
    internal class MediaCheckNotCompletedException : Exception
    {
        public MediaCheckNotCompletedException()
        {
        }

        public MediaCheckNotCompletedException(string? message) : base(message)
        {
        }

        public MediaCheckNotCompletedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MediaCheckNotCompletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}