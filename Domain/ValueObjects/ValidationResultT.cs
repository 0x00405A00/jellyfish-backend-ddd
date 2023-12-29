
using Domain.Errors;
using Domain.Primitives;

namespace Domain.ValueObjects
{
    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        public Error[] Errors { get; }
        public ValidationResult(Error[] errors) : base()
        {
            Errors = errors;
        }
        public static ValidationResult<TValue> WithErros(Error[] errors) => new(errors);
    }
}
