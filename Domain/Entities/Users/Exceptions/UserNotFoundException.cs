namespace Domain.Entities.Users.Exceptions
{
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId)
            : base($"User with id: {userId} not found")
        {

        }
        public UserNotFoundException(string activationToken)
            : base($"User with activationToken: {activationToken} not found")
        {

        }
    }
}
