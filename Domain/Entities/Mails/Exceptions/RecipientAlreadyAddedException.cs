using System.Runtime.Serialization;

namespace Domain.Entities.Mails.Exceptions
{
    public class RecipientAlreadyAddedException : Exception
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

        public RecipientAlreadyAddedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecipientAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}