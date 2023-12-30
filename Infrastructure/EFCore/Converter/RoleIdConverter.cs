using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
