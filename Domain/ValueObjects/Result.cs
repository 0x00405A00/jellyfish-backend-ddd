namespace Domain.ValueObjects
{
    public class Result 
    {
        public bool IsSuccess { get; protected set; }
        public Error.Error Error { get; set; }
        public Meta Meta { get; protected set; }

        public static Result Success()
        {
            var result = new Result();
            result.IsSuccess = true;
            return result;
        }
        public static Result Failure(string errorMessage, Domain.Error.Error.ERROR_CODE errorCode = Domain.Error.Error.ERROR_CODE.BadRequest)
        {
            var result = new Result();
            result.Error = new Domain.Error.Error(errorMessage, errorCode);
            return result;
        }
    }
    public class Result<T> : Result
    {
        public T Value { get; private set; }
        private Result(T? value=default)
        {
            Value = value;
        }
        public Result()
        {

        }
        public new static Result<T> Success(T? value)
        {
            var result = new Result<T>(value);
            result.IsSuccess = true;
            return result;
        }
        public new static Result<T> Success(T? value,Meta meta)
        {
            var result = new Result<T>(value);
            result.IsSuccess = true;
            result.Meta = meta;
            return result;
        }
        public new static Result<T> Failure(string errorMessage, Domain.Error.Error.ERROR_CODE errorCode= Domain.Error.Error.ERROR_CODE.BadRequest)
        {
            var result = new Result<T>();
            result.Error = new Domain.Error.Error(errorMessage, errorCode);
            return result;
        }
    }

}
