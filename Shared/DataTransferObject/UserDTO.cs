using Shared.MimePart;
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

        [JsonPropertyName("picture_mime_type")]
        public string? PictureMimeType{ get; set; }

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

        [JsonIgnore]
        public string? RoleStr
        {
            get
            {
                if(Roles!=null && Roles.Count>0)
                {
                    string str =null;
                    foreach(var role in Roles)
                    {
                        str+= role.Name+";";
                    }
                    return str;
                }
                return null;
            }
        }

        [JsonIgnore]
        public bool HasImage { get { return !String.IsNullOrEmpty(PictureBase64); } }

        public UserDTO()
        {

        }

        public void ResetFieldsForUserInterface()
        {
            this.Password = null;
            this.PasswordConfirm = null;
        }
        public string GetInitials()
        {
            var firstNameFirstLetter = FirstName != null ? FirstName.Substring(0, 1) : null;
            var lastNameFirstLetter = LastName != null ? LastName.Substring(0, 1) : null;
            var userNameFirstLetter = UserName != null ? UserName.Substring(0, 2) : null;
            return (firstNameFirstLetter !=null&& lastNameFirstLetter!=null)? 
                (firstNameFirstLetter+"."+ lastNameFirstLetter+ ".").ToUpper() :(userNameFirstLetter??"u").ToUpper();//U==UKNOWN
        }


    }
}
