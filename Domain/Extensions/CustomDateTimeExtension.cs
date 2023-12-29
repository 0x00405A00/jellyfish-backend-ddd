using Domain.Primitives;

namespace Domain.Extension
{
    public static class CustomDateTimeExtension
    {
        public static CustomDateTime ToTypedDateTime(this DateTime dateTime)
        {
            return new CustomDateTime(dateTime);
        }
    }
}
