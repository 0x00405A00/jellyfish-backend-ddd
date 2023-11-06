namespace Domain.Primitives
{
    public class BaseException : Exception
    {
        public string Title { get; }
        public string Message { get; }
        public string Details { get; }
        public int Code { get; }

        public BaseException(string title, string message, string details, int code) : base(message)
        {
            Title = title;
            Message = message;
            Details = details;
            Code = code;
        }
    }
}
