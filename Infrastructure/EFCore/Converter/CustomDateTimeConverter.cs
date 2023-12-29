using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Primitives;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.Converter
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
