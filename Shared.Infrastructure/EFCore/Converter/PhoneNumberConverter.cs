using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter
{
    public class PhoneNumberConverter : ValueConverter<PhoneNumber, string>
    {
        public PhoneNumberConverter()
            : base(
                v => v.PhoneNumb,
                v => PhoneNumber.Parse(v))
        {
        }
    }
}
