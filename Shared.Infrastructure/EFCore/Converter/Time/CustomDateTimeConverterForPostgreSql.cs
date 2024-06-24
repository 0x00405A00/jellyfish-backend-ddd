using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter.Time
{
    public class CustomDateTimeConverterForPostgreSql : ValueConverter<CustomDateTime, DateTimeOffset>
    {
        public CustomDateTimeConverterForPostgreSql()
            : base(
                v => v.DateTime,
                v => new CustomDateTime(v.DateTime))
        {
        }
    }
}
