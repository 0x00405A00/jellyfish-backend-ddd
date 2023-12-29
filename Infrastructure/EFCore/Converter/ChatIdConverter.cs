using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.Converter
{
    public class ChatIdConverter : ValueConverter<ChatId, Guid>
    {
        public ChatIdConverter()
            : base(
                v => v.Id,
                v => new ChatId(v))
        {
        }
    }
}
