using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Result 
    {
        public bool IsSuccess { get; protected set; }
        public Error.Error Error { get; set; }

        public static Result Success()
        {
            var result = new Result();
            result.IsSuccess = true;
            return result;
        }
        public static Result Failure(string errorMessage)
        {
            var result = new Result();
            result.Error = new Domain.Error.Error(errorMessage);
            return result;
        }
    }
    public class Result<T> : Result
    {
        public T Value { get; set; }

        public new static Result<T> Success()
        {
            var result = new Result<T>();
            result.IsSuccess = true;
            return result;
        }
        public new static Result<T> Failure(string errorMessage)
        {
            var result = new Result<T>();
            result.Error = new Domain.Error.Error(errorMessage);
            return result;
        }
    }

}
