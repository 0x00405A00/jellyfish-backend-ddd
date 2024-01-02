using Domain.Errors;
using System.Collections.Immutable;

namespace Domain.ValueObjects
{
    public class Result 
    {
        public bool IsSuccess { get; protected set; }
        protected List<Error> _errors { get; private set; } = new();
        public IReadOnlyCollection<Error> Errors => _errors.ToImmutableList();

        public Meta Meta { get; protected set; }

        public void AddError(Error error)
        {
            if (error is null)
                throw new ArgumentNullException(nameof(error));

            _errors.Add(error);
        }
        public void AddError(ICollection<Error> error)
        {
            if (error is null || error.Any(x=>x is null))
                throw new ArgumentNullException(nameof(error));

            _errors.AddRange(error);
        }
        public static Result Success()
        {
            var result = new Result();
            result.IsSuccess = true;
            return result;
        }
        public static Result Failure(string errorMessage, Error.ERROR_CODE errorCode = Domain.Errors.Error.ERROR_CODE.BadRequest)
        {
            var result = new Result();
            var err = new Error(errorMessage, errorCode);
            result._errors.Add(err);
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
        public new static Result<T> Failure(string errorMessage, Error.ERROR_CODE errorCode = Domain.Errors.Error.ERROR_CODE.BadRequest)
        {
            var result = new Result<T>();
            result._errors.Add(new Error(errorMessage, errorCode));
            return result;
        }
        public new static Result<T> Failure(string errorMessage, List<Error> errors)
        {
            var result = new Result<T>();
            result.AddError(errors);
            return result;
        }
    }

}
