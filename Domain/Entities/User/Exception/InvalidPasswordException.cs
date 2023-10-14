namespace Domain.Entities.User.Exception
{
    public class InvalidPasswordException : System.Exception
    {
        public string[] Errors { get; }

        public InvalidPasswordException(string error)
        {
            Errors = new string[] { error };
        }
        public InvalidPasswordException(string[] errors)
        {
            Errors = errors;
        }

        public override string ToString()
        {
            string tmp = string.Empty;
            for(int i=0;i< Errors.Length;i++)
            {
                tmp += Errors[i];
            }
            return tmp;
        }

    }
}
