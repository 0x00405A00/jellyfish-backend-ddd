using System.Runtime.Serialization;

namespace Domain.Entities.User.Exception
{
    public class UserAlreadyDeletedException : System.Exception
    {
        public UserAlreadyDeletedException()
        {
        }

        public UserAlreadyDeletedException(string? message) : base(message)
        {
        }

        public UserAlreadyDeletedException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

    }
}