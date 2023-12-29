namespace Domain.Entities.Chats.Exceptions
{
    public class UserIsNoMemberInChatException : Exception
    {
        public UserIsNoMemberInChatException(string? message) : base(message)
        {
        }
    }
}
