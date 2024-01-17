using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter
{
    public class UserTypeIdConverter : ValueConverter<UserTypeId, Guid>
    {
        public UserTypeIdConverter()
            : base(
                v => v.Id,
                v => new UserTypeId(v))
        {
        }
    }
}
