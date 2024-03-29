﻿using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Shared.Infrastructure.EFCore.Converter
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
