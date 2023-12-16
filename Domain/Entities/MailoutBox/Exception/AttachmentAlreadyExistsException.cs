using System.Runtime.Serialization;

namespace Domain.Entities.MailoutBox.Exception
{
    public class AttachmentAlreadyExistsException : System.Exception
    {
        public AttachmentAlreadyExistsException()
        {
        }

        public AttachmentAlreadyExistsException(string? message) : base(message)
        {
        }


        public AttachmentAlreadyExistsException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected AttachmentAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}