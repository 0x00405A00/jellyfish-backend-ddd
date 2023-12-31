using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObject.Abstraction
{
    public class AbstractAuditableDTO : AbstractDTO,IAuditableEntity
    {
        [JsonPropertyName("created_by_user_uuid")]
        public Guid? CreatedByUserForeignKey { get; set; }

        [JsonPropertyName("last_modified_by_user_uuid")]
        public Guid? LastModifiedByUserForeignKey { get; set; }

        [JsonPropertyName("deleted_by_user_uuid")]
        public Guid? DeletedByUserForeignKey { get; set; }

        public User? CreatedByUser { get; set; }
        public User? DeletedByUser { get; set; }
        public User? LastModifiedByUser { get; set; }
        UserId? IAuditableEntity.CreatedByUserForeignKey { get; set; }
        UserId? IAuditableEntity.DeletedByUserForeignKey { get; set; }
        UserId? IAuditableEntity.LastModifiedByUserForeignKey { get; set; }
    }
}
