namespace Domain.Entities.Users.Exceptions
{
    public class RemoveFriendException : Exception
    {
        public RemoveFriendException(string? message) : base(message)
        {
        }
    }
}
