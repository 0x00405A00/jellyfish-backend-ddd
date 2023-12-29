using Domain.Errors;
using Domain.Primitives;

namespace Domain.ValueObjects
{
    public sealed class ValidationResult : Result, IValidationResult
    {
        public Error[] Errors { get; }
        public ValidationResult(Error[] errors) : base() 
        {
            Errors = errors;
        }
        public static ValidationResult WithErros(Error[] errors) => new(errors);
    }
}
