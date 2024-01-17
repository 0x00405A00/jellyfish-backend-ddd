using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter
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
