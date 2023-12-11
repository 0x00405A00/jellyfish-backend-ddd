namespace Domain.Entities.Chats.Exception
{
    public class UserAlreadyMemberInChatException : System.Exception
    {
        public UserAlreadyMemberInChatException(string? message) : base(message)
        {
            
        }
    }
}
