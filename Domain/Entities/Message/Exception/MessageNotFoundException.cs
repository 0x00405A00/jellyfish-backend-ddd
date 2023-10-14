using System.Runtime.Serialization;

namespace Domain.Entities.Message.Exception
{
    public class MessageNotFoundException : System.Exception
    {
        public MessageNotFoundException()
        {
        }

        public MessageNotFoundException(string? message) : base(message)
        {
        }

        public MessageNotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected MessageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
