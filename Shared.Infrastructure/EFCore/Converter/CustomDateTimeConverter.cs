using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter
{
    public class CustomDateTimeConverter : ValueConverter<CustomDateTime, DateTimeOffset>
    {
        public CustomDateTimeConverter()
            : base(
                v => v.DateTime,
                v => new CustomDateTime(v.DateTime))
        {
        }
    }
}
