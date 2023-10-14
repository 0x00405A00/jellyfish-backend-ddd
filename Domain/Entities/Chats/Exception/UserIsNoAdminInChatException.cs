namespace Domain.Entities.Chats.Exception
{
    public class UserIsNoAdminInChatException : System.Exception
    {
        public UserIsNoAdminInChatException(string? message) : base(message)
        {
        }
    }
}
