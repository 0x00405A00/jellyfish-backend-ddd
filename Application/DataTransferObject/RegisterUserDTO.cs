using System.Text.Json.Serialization;

namespace Application.DataTransferObject
{
    public class RegisterUserDTO : AbstractUserDTO
    {
        [JsonPropertyName("user_name")]
        public string? UserName { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("picture_base64")]
        public string? PictureBase64 { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateOnly? DateOfBirth { get; set; }


        public RegisterUserDTO()
        {

        }
    }
}
