namespace Domain.Entities.Chats.Exceptions
{
    public class UserIsNoAdminInChatException : System.Exception
    {
        public UserIsNoAdminInChatException(string? message) : base(message)
        {
        }
    }
}
