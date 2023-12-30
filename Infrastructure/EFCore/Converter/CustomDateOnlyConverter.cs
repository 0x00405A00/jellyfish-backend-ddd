using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.Converter
{
    public class CustomDateOnlyConverter : ValueConverter<CustomDateOnly, DateOnly>
    {
        public CustomDateOnlyConverter()
            : base(
                v => v.Date,
                v => new CustomDateOnly(v))
        {
        }
    }
}
