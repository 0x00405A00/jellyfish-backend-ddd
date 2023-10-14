namespace Domain.Entities.Chats.Exception
{
    public class UserIsNoMemberInChatException : System.Exception
    {
        public UserIsNoMemberInChatException(string? message) : base(message)
        {
        }
    }
}
