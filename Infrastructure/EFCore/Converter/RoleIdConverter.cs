using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.Converter
{
    public class RoleIdConverter : ValueConverter<RoleId, Guid>
    {
        public RoleIdConverter()
            : base(
                v => v.Id,
                v => new RoleId(v))
        {
        }
    }
}
