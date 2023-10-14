using Domain.Error;
using Domain.Primitives;

namespace Domain.ValueObjects
{
    public sealed class ValidationResult : Result, IValidationResult
    {
        public Error.Error[] Errors { get; }
        public ValidationResult(Error.Error[] errors) : base() 
        {
            Errors = errors;
        }
        public static ValidationResult WithErros(Error.Error[] errors) => new(errors);
    }
}
