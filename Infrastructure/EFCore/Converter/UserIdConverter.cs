using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.Converter
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
