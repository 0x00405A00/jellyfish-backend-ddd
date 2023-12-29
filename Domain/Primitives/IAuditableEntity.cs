using Domain.Entities.Users;
using Shared.ValueObjects.Ids;

namespace Domain.Primitives
{
    public interface IAuditableEntity
    {
        User? CreatedByUser { get; set; }
        UserId? CreatedByUserForeignKey { get; set; }
        User? DeletedByUser { get; set; }
        UserId? DeletedByUserForeignKey { get; set; }
        User? LastModifiedByUser { get; set; }
        UserId? LastModifiedByUserForeignKey { get; set; }
    }
}
