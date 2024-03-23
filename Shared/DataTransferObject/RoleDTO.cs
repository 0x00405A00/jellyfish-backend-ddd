using System.Text.Json.Serialization;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class RoleDTO : AbstractAuditableDTO, IRole
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public IReadOnlyCollection<UserHasRelationToRole> UserHasRelationToRoles { get; }

        public override string ToString() => Name;
    }
}