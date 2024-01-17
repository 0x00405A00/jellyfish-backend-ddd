using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter
{
    public class UserIdConverter : ValueConverter<UserId, Guid>
    {
        public UserIdConverter()
            : base(
                v => v.Id,
                v => new UserId(v))
        {
        }
    }
}
