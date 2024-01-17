using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter
{
    public class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter()
            : base(
                v => v.EmailValue,
                v => Email.Parse(v))
        {
        }
    }
}
