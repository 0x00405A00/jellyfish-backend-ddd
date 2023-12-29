namespace Domain.Entities.Chats.Exceptions
{
    public class NotValidChatDescriptionException : System.Exception
    {
        public NotValidChatDescriptionException(string? message) : base(message)
        {
        }
    }
}
