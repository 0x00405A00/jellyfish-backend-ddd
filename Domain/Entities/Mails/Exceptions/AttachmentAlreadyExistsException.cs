using System.Runtime.Serialization;

namespace Domain.Entities.Mails.Exceptions
{
    public class AttachmentAlreadyExistsException : Exception
    {
        public AttachmentAlreadyExistsException()
        {
        }

        public AttachmentAlreadyExistsException(string? message) : base(message)
        {
        }


        public AttachmentAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AttachmentAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}