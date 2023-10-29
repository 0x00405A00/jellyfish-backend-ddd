using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class UserDTO : AbstractUserDTO
    {
        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; }

        [JsonPropertyName("user_name")]
        public string? UserName { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("password_repeat")]
        public string? PasswordConfirm { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("picture_base64")]
        public string? PictureBase64 { get; set; }

        [JsonPropertyName("user_type_uuid")]
        public Guid? UserTypeUuid { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [JsonPropertyName("activation_date_time")]
        public DateTime? ActivationDateTime { get; set; }

        [JsonPropertyName("roles")]
        public ICollection<RoleDTO>? Roles { get; set; }

        [JsonPropertyName("friends")]
        public ICollection<Guid>? Friends { get; set; }

        [JsonPropertyName("friendship_requests")]
        public ICollection<FriendshipRequestDTO>? FriendshipRequests { get; set; }

        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        [JsonPropertyName("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }

        [JsonPropertyName("deleted_time")]
        public DateTime? DeletedTime { get; set; }

        [JsonPropertyName("created_by_user_uuid")]
        public Guid? CreatedByUserUuid { get; set; }

        [JsonPropertyName("last_modified_by_user_uuid")]
        public Guid? LastModifiedByUserUuid { get; set; }

        [JsonPropertyName("deleted_by_user_uuid")]
        public Guid? DeletedByUserUuid { get; set; }

        public UserDTO()
        {

        }
    }
}
