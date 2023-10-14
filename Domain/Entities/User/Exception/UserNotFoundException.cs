namespace Domain.Entities.User.Exception
{
    public sealed class UserNotFoundException : System.Exception
    {
        public UserNotFoundException(Guid userId)
            : base($"User with id: {userId} not found")
        {

        }
    }
}
