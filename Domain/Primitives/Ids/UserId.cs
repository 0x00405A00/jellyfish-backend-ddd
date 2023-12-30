﻿using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record UserId : Identification
    {
        public UserId(Guid guid) : base(guid)
        {
        }
    }
}
