namespace Domain.Entities.Users.Exceptions
{
    public class InvalidUserNameException : Exception
    {
        public InvalidUserNameException(string message):base(message)
        {
            
        }
    }
}
