namespace Domain.Entities.Users.Exceptions
{
    public class RemoveRoleException : Exception
    {
        public RemoveRoleException(string? message) : base(message)
        {
        }
    }
}
