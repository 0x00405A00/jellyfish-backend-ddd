using System.Runtime.Serialization;

namespace Domain.Entities.Users.Exceptions
{
    public class UserAlreadyDeletedException : Exception
    {
        public UserAlreadyDeletedException()
        {
        }

        public UserAlreadyDeletedException(string? message) : base(message)
        {
        }

        public UserAlreadyDeletedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}