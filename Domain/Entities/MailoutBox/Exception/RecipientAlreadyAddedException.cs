using System.Runtime.Serialization;

namespace Domain.Entities.MailoutBox.Exception
{
    public class RecipientAlreadyAddedException : System.Exception
    {

        public RecipientAlreadyAddedException()
        {
        }

        public RecipientAlreadyAddedException(string? message) : base(message)
        {
        }

        public RecipientAlreadyAddedException(string v1, string v2)
        {

        }

        public RecipientAlreadyAddedException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected RecipientAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}