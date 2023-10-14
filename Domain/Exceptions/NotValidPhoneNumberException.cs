namespace Domain.Exceptions
{
    public sealed class NotValidPhoneNumberException : Exception
    {
        public NotValidPhoneNumberException(string phoneNumber) 
            : base($"{phoneNumber} is not valid")
        {
            
        }
    }
}
