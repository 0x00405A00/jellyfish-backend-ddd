using System.Text.Json.Serialization;
using Domain.Entities.Users;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class UserTypeDTO: AbstractAuditableDTO, IUserType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public UserTypeDTO()
        {

        }

        public override string ToString() => Name;
    }
}
