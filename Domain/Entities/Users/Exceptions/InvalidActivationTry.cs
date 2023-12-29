using System.Runtime.Serialization;

namespace Domain.Entities.Users.Exceptions
{
    [Serializable]
    public class InvalidActivationTry : Exception
    {
        public InvalidActivationTry()
        {
        }

        public InvalidActivationTry(string? message) : base(message)
        {
        }

        public InvalidActivationTry(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidActivationTry(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}