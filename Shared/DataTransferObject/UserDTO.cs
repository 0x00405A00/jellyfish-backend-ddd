using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Shared.DataTransferObject.Abstraction;
using Shared.Http;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class UserDTO : AbstractUserDTO, ICloneable, IUser
    {
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
        [JsonPropertyName("picture_url")]
        public string? PictureUrl { get; set; }

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
        public bool HasImage { get { return (!String.IsNullOrEmpty(PictureUrl) && Extension.IsValidUrl(PictureUrl) || (!String.IsNullOrEmpty(PictureBase64) && !String.IsNullOrEmpty(PictureMimeType))); } }

        [JsonIgnore]
        public string ActivationCode{ get; }

        [JsonIgnore]
        CustomDateTime? IUser.ActivationDateTime{ get; }

        [JsonIgnore]
        public string ActivationToken{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<ChatInviteRequest>? ChatInvitesWhereIamRequester{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<ChatInviteRequest>? ChatInvitesWhereIamTarget{ get; }

        [JsonIgnore]
        CustomDateTime IUser.DateOfBirth{ get; }

        [JsonIgnore]
        Email IUser.Email{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<FriendshipRequest>? FriendshipRequestsWhereIamRequester{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<FriendshipRequest>? FriendshipRequestsWhereIamTarget{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<UserHasRelationToFriend>? FriendshipsThatIAccepted{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<UserHasRelationToFriend>? FriendshipsThatIRequested{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<Message>? Messages{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<MessageOutbox>? MessagesInOutbox{ get; }

        [JsonIgnore]
        PhoneNumber IUser.Phone{ get; }

        [JsonIgnore]
        public Picture Picture{ get; }

        [JsonIgnore]
        public IReadOnlyCollection<UserHasRelationToRole> UserHasRelationToRoles{ get; }

        [JsonIgnore]
        public UserType UserType { get; set; }

        [JsonIgnore]
        public UserTypeId UserTypeForeignKey{ get; }

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
                (firstNameFirstLetter+""+ lastNameFirstLetter+ "").ToUpper() :(userNameFirstLetter??"u").ToUpper();//U==UKNOWN
        }

        public object Clone()
        {

            return this.MemberwiseClone();
        }
    }
}
