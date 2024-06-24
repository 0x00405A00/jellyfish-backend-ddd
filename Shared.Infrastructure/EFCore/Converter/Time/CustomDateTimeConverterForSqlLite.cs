using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter.Time
{
    public class CustomDateTimeConverterForSqlLite : ValueConverter<CustomDateTime, DateTime>
    {
        public CustomDateTimeConverterForSqlLite()
            : base(
                v => v.DateTime,
                v => new CustomDateTime(v))
        {
        }
    }
}
