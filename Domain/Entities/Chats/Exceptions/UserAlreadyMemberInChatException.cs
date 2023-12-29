namespace Domain.Entities.Chats.Exceptions
{
    public class UserAlreadyMemberInChatException : System.Exception
    {
        public UserAlreadyMemberInChatException(string? message) : base(message)
        {

        }
    }
}
