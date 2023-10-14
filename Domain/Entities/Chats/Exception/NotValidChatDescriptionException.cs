namespace Domain.Entities.Chats.Exception
{
    public class NotValidChatDescriptionException : System.Exception
    {
        public NotValidChatDescriptionException(string? message) : base(message)
        {
        }
    }
}
