using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EFCore.Converter
{
    public class CustomDateTimeConverter : ValueConverter<CustomDateTime, DateTime>
    {
        public CustomDateTimeConverter()
            : base(
                v => v.DateTime,
                v => new CustomDateTime(v))
        {
        }
    }
}
