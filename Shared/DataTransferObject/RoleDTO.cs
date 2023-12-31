using System.Text.Json.Serialization;
using Domain.Entities.Roles;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class RoleDTO : AbstractAuditableDTO, IRole
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}