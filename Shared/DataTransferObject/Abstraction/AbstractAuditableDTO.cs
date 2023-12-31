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

        public User? CreatedByUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public User? DeletedByUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public User? LastModifiedByUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        UserId? IAuditableEntity.CreatedByUserForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        UserId? IAuditableEntity.DeletedByUserForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        UserId? IAuditableEntity.LastModifiedByUserForeignKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
