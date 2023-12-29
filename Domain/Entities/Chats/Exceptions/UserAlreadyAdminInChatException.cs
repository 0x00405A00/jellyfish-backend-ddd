using System.Runtime.Serialization;

namespace Domain.Entities.Chats.Exceptions
{
    public class UserAlreadyAdminInChatException : System.Exception
    {
        public UserAlreadyAdminInChatException(string? message) : base(message)
        {
        }

        public UserAlreadyAdminInChatException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}