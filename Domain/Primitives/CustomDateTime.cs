namespace Domain.Primitives
{
    public record CustomDateTime(DateTime DateTime)
    {
        public static CustomDateTime Now() 
        { 
            return new CustomDateTime(DateTime.UtcNow);
        }
    }
}
