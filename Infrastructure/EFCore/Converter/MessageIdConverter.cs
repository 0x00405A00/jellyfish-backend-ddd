using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.Converter
{
    public class MessageIdConverter : ValueConverter<MessageId, Guid>
    {
        public MessageIdConverter()
            : base(
                v => v.Id,
                v => new MessageId(v))
        {
        }
    }
}
