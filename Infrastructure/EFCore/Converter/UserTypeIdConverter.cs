using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.Converter
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
