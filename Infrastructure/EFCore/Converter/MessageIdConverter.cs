﻿using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EFCore.Converter
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
