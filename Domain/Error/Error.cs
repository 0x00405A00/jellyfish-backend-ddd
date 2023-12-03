namespace Domain.Error
{
    public class Error
    {
        public enum ERROR_CODE : int
        {
            BadRequest,
            Forbidden,
            UnproccesableEntity,
            Unauthorized,
            NotFound,
            Exception,

        }
        public string Message { get; private set; }
        public ERROR_CODE ErrorCode { get; }

        public Error(string message, ERROR_CODE errorCode = ERROR_CODE.BadRequest)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
