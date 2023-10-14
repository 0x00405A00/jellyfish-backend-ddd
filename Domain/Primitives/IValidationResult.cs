namespace Domain.Primitives
{
    public interface IValidationResult
    {
        public static readonly Error.Error ValidationError = new("A validation proble occured.");

        Error.Error[] Errors { get; }
    }
}
