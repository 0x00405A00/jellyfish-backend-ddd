namespace Domain.Entities.Chats.Exceptions
{
    public sealed class ChatNotFoundException : System.Exception
    {
        public ChatNotFoundException(Guid chatId)
            : base($"Chat {chatId} not found")
        {

        }
    }
}
