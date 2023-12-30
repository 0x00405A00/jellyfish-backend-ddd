using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EFCore.Converter
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
