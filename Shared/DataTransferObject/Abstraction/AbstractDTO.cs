using Domain.Primitives;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObject.Abstraction
{
    public class AbstractDTO : IDataTransferObject, IEntityBase
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }

        [JsonPropertyName("deleted_time")]
        public DateTime? DeletedTime { get; set; }

        [JsonIgnore]
        CustomDateTime IEntityBase.CreatedTime { get; set; }
        [JsonIgnore]
        CustomDateTime? IEntityBase.DeletedTime { get; set; }
        [JsonIgnore]
        CustomDateTime? IEntityBase.LastModifiedTime { get; set; }
    }
}
