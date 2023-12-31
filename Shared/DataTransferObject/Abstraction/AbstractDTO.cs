using Domain.Primitives;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObject.Abstraction
{
    public class AbstractDTO : IDataTransferObject, IEntityBase
    {
        [JsonPropertyName("uuid")]
        public Guid Id { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }

        [JsonPropertyName("deleted_time")]
        public DateTime? DeletedTime { get; set; }

        CustomDateTime IEntityBase.CreatedTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        CustomDateTime? IEntityBase.DeletedTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        CustomDateTime? IEntityBase.LastModifiedTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
