using Domain.Primitives;

namespace Domain.Extension
{
    public static class CustomDateOnlyExtension
    {
        public static CustomDateOnly ToTypedDateOnly(this DateOnly date)
        {
            return new CustomDateOnly(date);
        }
    }
}
