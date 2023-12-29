using Domain.Errors;

namespace Domain.Primitives
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new("A validation proble occured.");

        Error[] Errors { get; }
    }
}
