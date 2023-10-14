namespace Domain.Exceptions
{
    public sealed class NotValidEmailException : Exception
    {
        public NotValidEmailException(string email) 
            : base($"{email} is not valid")
        {
            
        }
    }
}
