using System.Runtime.Serialization;

namespace Shared.Infrastructure.FileSys
{
    [Serializable]
    internal class MediaViolatesPolicyException : Exception
    {
        public MediaViolatesPolicyException()
        {
        }

        public MediaViolatesPolicyException(string? message) : base(message)
        {
        }

        public MediaViolatesPolicyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MediaViolatesPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}