
using Domain.Primitives;

namespace Domain.ValueObjects
{
    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        public Error.Error[] Errors { get; }
        public ValidationResult(Error.Error[] errors) : base()
        {
            Errors = errors;
        }
        public static ValidationResult<TValue> WithErros(Error.Error[] errors) => new(errors);
    }
}
