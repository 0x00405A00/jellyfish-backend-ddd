using Domain.Primitives;

namespace Domain.Extension
{
    public static class CustomDateOnlyExtension
    {
        public static CustomDateTime ToTypedDateOnly(this DateTime date)
        {
            return new CustomDateTime(date);
        }
    }
}
